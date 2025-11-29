using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace ProjetoSOFinal {
    public partial class FormSistemaOperacional : Form {
        enum ThreadState { New, Ready, Running, Waiting, Terminated }
        enum SchedulerType { RoundRobin, FCFS, Prioridade }

        class SimThread {
            public int TID { get; set; }
            public int ParentPID { get; set; }
            public int TotalBurst { get; set; }
            public int Remaining { get; set; }
            public int PC { get; set; }
            public ThreadState State { get; set; }
            public int Priority { get; set; }
            public List<int> Pages { get; set; } = new List<int>();
            public int CurrentPageIndex { get; set; } = 0;
            public int QuantumUsed { get; set; } = 0;
            public int WaitingForIOTicks { get; set; } = 0;
            public string Cwd { get; set; } = "/";
            public List<VFile> OwnedFiles { get; set; } = new List<VFile>();
            public DateTime LastCpuUse { get; set; } = DateTime.MinValue;
            public override string ToString() => $"PID:{ParentPID} TID:{TID} State:{State} Rem:{Remaining}";
        }

        class SimProcess {
            public int PID { get; set; }
            public int Priority { get; set; }
            public List<SimThread> Threads { get; set; } = new List<SimThread>();
            public int ArrivalTick { get; set; }
            public bool IsFinished => Threads.All(t => t.State == ThreadState.Terminated);
            public int TotalRemaining => Threads.Sum(t => t.Remaining);
            public override string ToString() => $"PID:{PID} Pri:{Priority} Threads:{Threads.Count}";
        }

        class FrameEntry {
            public int FrameIndex { get; set; }
            public string Owner { get; set; }
            public int Page { get; set; }
            public string Estado { get; set; }
            public DateTime LastUsed { get; set; } = DateTime.MinValue;
        }

        class VFile {
            public string Name { get; set; }
            public int Size { get; set; }
            public DateTime Modified { get; set; }
            public string Path { get; set; }
            public override string ToString() => $"{Name} ({Size}B)";
        }

        class VDirectory {
            public string Name { get; set; }
            public string Path { get; set; }
            public List<VDirectory> Dirs { get; set; } = new List<VDirectory>();
            public List<VFile> Files { get; set; } = new List<VFile>();
        }

        int nextPID = 1;
        int nextTID = 1;
        List<SimProcess> processos = new List<SimProcess>();
        Queue<SimThread> readyQueue = new Queue<SimThread>();
        List<SimThread> waitingList = new List<SimThread>();
        List<SimThread> finishedThreads = new List<SimThread>();
        SimThread runningThread = null;
        List<FrameEntry> frames = new List<FrameEntry>();
        int totalFrames = 64;
        int tickCount = 0;
        int ticksCpuInUse = 0;
        SchedulerType scheduler = SchedulerType.RoundRobin;
        bool running = false;
        int ioDeviceTicksPerOperation = 5;
        List<(SimThread thread, int remaining, string reason)> ioQueue = new List<(SimThread, int, string)>();
        int quantum => Math.Max(1, Decimal.ToInt32(numericUpDownValorQuantum.Value));
        VDirectory fsRoot;
        Random rnd = new Random();
        ContextMenuStrip fsContextMenu;
        Dictionary<string, Color> ownerColor = new Dictionary<string, Color>();
        const int MinCellSize = 8;
        private DataGridView dataGridViewIODevices;

        public FormSistemaOperacional() {
            InitializeComponent();
        }

        private void FormSistemaOperacional_Load(object sender, EventArgs e) {
            comboBoxEscalonador.SelectedIndex = 0;
            timerSO.Interval = 200;
            InitializeMemory();
            InitializeFilesystem();
            SetupFilesystemContextMenu();
            InitializeIODevicesView();
            panelVisualizacaoMemoria.Paint += PanelVisualizacaoMemoria_Paint;
            panelVisualizacaoMemoria.Resize += (s, ev) => panelVisualizacaoMemoria.Invalidate();
            treeViewSistemaArquivos.NodeMouseClick += TreeViewSistemaArquivos_NodeMouseClick;
            UpdateAllViews();
            dataGridViewProcessos.SelectionChanged += DataGridViewProcessos_SelectionChanged;
            tabControlTabelasInformacoes.SelectedIndexChanged += TabControlTabelasInformacoes_SelectedIndexChanged;
            RefreshMemoryView();
        }

        private void InitializeMemory() {
            frames.Clear();
            for (int i = 0; i < totalFrames; i++) {
                frames.Add(new FrameEntry { FrameIndex = i, Owner = "-", Page = -1, Estado = "Livre", LastUsed = DateTime.MinValue });
            }
        }

        private void InitializeFilesystem() {
            fsRoot = new VDirectory { Name = "root", Path = "/" };
            EnsureDirectory("/bin");
            EnsureDirectory("/etc");
            EnsureDirectory("/home/user");
            CreateFile("/home/user/document.txt", rnd.Next(10, 500));
            CreateFile("/home/user/program.exe", rnd.Next(100, 2000));
            RefreshTreeViewFromFs();
        }

        private VDirectory EnsureDirectory(string path) {
            var parts = path.Trim('/').Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            VDirectory cur = fsRoot;
            string accum = "";
            foreach (var p in parts) {
                accum = accum + "/" + p;
                var found = cur.Dirs.FirstOrDefault(d => d.Name == p);
                if (found == null) {
                    found = new VDirectory { Name = p, Path = accum == "" ? "/" : accum };
                    cur.Dirs.Add(found);
                }
                cur = found;
            }
            return cur;
        }

        private VFile CreateFile(string path, int size) {
            var p = path.Replace("\\", "/");
            var idx = p.LastIndexOf('/');
            string dir = "/";
            string name = p;
            if (idx >= 0) {
                dir = p.Substring(0, idx);
                name = p.Substring(idx + 1);
            }
            var d = EnsureDirectory(dir == "" ? "/" : dir);
            var existing = d.Files.FirstOrDefault(x => x.Name == name);
            if (existing != null) {
                existing.Size = size;
                existing.Modified = DateTime.Now;
                RefreshTreeViewFromFs();
                return existing;
            }
            var f = new VFile { Name = name, Size = size, Modified = DateTime.Now, Path = (dir == "" ? "/" : dir) + "/" + name };
            d.Files.Add(f);
            RefreshTreeViewFromFs();
            return f;
        }

        private bool RemoveFile(string path) {
            var p = path.Replace("\\", "/");
            var idx = p.LastIndexOf('/');
            string dir = "/";
            string name = p;
            if (idx >= 0) {
                dir = p.Substring(0, idx);
                name = p.Substring(idx + 1);
            }
            var d = GetDirectory(dir == "" ? "/" : dir);
            if (d == null) return false;
            var f = d.Files.FirstOrDefault(x => x.Name == name);
            if (f == null) return false;
            d.Files.Remove(f);
            foreach (var proc in processos) {
                foreach (var t in proc.Threads) {
                    t.OwnedFiles.RemoveAll(of => of.Path == f.Path);
                }
            }
            RefreshTreeViewFromFs();
            Log($"Arquivo removido: {f.Path}");
            return true;
        }

        private VDirectory GetDirectory(string path) {
            if (path == "/") return fsRoot;
            var parts = path.Trim('/').Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            VDirectory cur = fsRoot;
            foreach (var p in parts) {
                var found = cur.Dirs.FirstOrDefault(d => d.Name == p);
                if (found == null) return null;
                cur = found;
            }
            return cur;
        }

        private bool RemoveDirectory(string path) {
            if (path == "/") return false;
            var idx = path.TrimEnd('/').LastIndexOf('/');
            string parent = "/";
            string name = path.Trim('/');
            if (idx >= 0) {
                parent = path.Substring(0, idx);
                name = path.Substring(idx + 1).Trim('/');
            }
            var pd = GetDirectory(parent == "" ? "/" : parent);
            if (pd == null) return false;
            var d = pd.Dirs.FirstOrDefault(x => x.Name == name);
            if (d == null) return false;
            if (d.Dirs.Count > 0 || d.Files.Count > 0) return false;
            pd.Dirs.Remove(d);
            RefreshTreeViewFromFs();
            Log($"Diretório removido: {d.Path}");
            return true;
        }

        private void RefreshTreeViewFromFs() {
            treeViewSistemaArquivos.Nodes.Clear();
            TreeNode MakeNode(VDirectory d) {
                var n = new TreeNode(d.Name) { Tag = d.Path };
                foreach (var sd in d.Dirs) n.Nodes.Add(MakeNode(sd));
                foreach (var f in d.Files) n.Nodes.Add(new TreeNode(f.Name) { Tag = f.Path });
                return n;
            }
            treeViewSistemaArquivos.Nodes.Add(MakeNode(fsRoot));
            treeViewSistemaArquivos.ExpandAll();
        }

        private void SetupFilesystemContextMenu() {
            fsContextMenu = new ContextMenuStrip();
            var criarArquivo = new ToolStripMenuItem("Criar arquivo");
            var removerArquivo = new ToolStripMenuItem("Remover arquivo");
            var criarPasta = new ToolStripMenuItem("Criar pasta");
            var removerPasta = new ToolStripMenuItem("Remover pasta (vazia)");
            criarArquivo.Click += (s, e) => FsContextCreateFile();
            removerArquivo.Click += (s, e) => FsContextRemoveFile();
            criarPasta.Click += (s, e) => FsContextCreateDir();
            removerPasta.Click += (s, e) => FsContextRemoveDir();
            fsContextMenu.Items.AddRange(new ToolStripItem[] { criarArquivo, removerArquivo, criarPasta, removerPasta });
        }

        private void TreeViewSistemaArquivos_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) {
            treeViewSistemaArquivos.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right) {
                fsContextMenu.Show(treeViewSistemaArquivos, e.Location);
            }
        }

        private void FsContextCreateFile() {
            var node = treeViewSistemaArquivos.SelectedNode;
            if (node == null) return;
            string dirPath = node.Tag as string;
            if (dirPath == null) {
                var parent = node.Parent;
                if (parent != null) dirPath = parent.Tag as string;
                else dirPath = "/";
            }
            if (dirPath == null) dirPath = "/";
            string name = Interaction.InputBox("Nome do arquivo:", "Criar arquivo", "novo.txt");
            if (string.IsNullOrWhiteSpace(name)) return;
            var path = (dirPath == "/" ? "" : dirPath) + "/" + name;
            var file = CreateFile(path, rnd.Next(1, 500));
            Log($"Arquivo criado: {file.Path}");
            RefreshTreeViewFromFs();
        }

        private void FsContextRemoveFile() {
            var node = treeViewSistemaArquivos.SelectedNode;
            if (node == null) return;
            var tag = node.Tag as string;
            if (tag == null) return;
            if (!tag.Contains('/')) return;
            var idx = tag.LastIndexOf('/');
            if (idx < 0) return;
            if (RemoveFile(tag)) UpdateAllViews();
            else MessageBox.Show("Não foi possível remover (talvez seja um diretório).");
        }

        private void FsContextCreateDir() {
            var node = treeViewSistemaArquivos.SelectedNode;
            if (node == null) return;
            string dirPath = node.Tag as string;
            if (dirPath == null) {
                var parent = node.Parent;
                if (parent != null) dirPath = parent.Tag as string;
                else dirPath = "/";
            }
            string name = Interaction.InputBox("Nome da pasta:", "Criar pasta", "nova_pasta");
            if (string.IsNullOrWhiteSpace(name)) return;
            var newPath = (dirPath == "/" ? "" : dirPath) + "/" + name;
            EnsureDirectory(newPath);
            Log($"Pasta criada: {newPath}");
            RefreshTreeViewFromFs();
        }

        private void FsContextRemoveDir() {
            var node = treeViewSistemaArquivos.SelectedNode;
            if (node == null) return;
            var tag = node.Tag as string;
            if (tag == null) return;
            if (RemoveDirectory(tag)) UpdateAllViews();
            else MessageBox.Show("Não foi possível remover a pasta. Ela pode não estar vazia ou ser root.");
        }

        private void buttonCarregarArquivo_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK) {
                try {
                    var lines = File.ReadAllLines(ofd.FileName);
                    ResetSimulationInternal();
                    foreach (var raw in lines) {
                        var line = raw.Trim();
                        if (string.IsNullOrEmpty(line)) continue;
                        var p = ParseProcessLine(line);
                        processos.Add(p);
                        foreach (var t in p.Threads) EnqueueReady(t);
                    }
                    Log("Arquivo carregado. Processos criados.");
                }
                catch {
                    ResetSimulationInternal();
                    GenerateSampleProcesses();
                    Log("Erro ao ler arquivo. Gerados processos de amostra.");
                }
            }
            else {
                ResetSimulationInternal();
                GenerateSampleProcesses();
                Log("Nenhum arquivo selecionado. Gerados processos de amostra.");
            }
            UpdateAllViews();
        }

        private SimProcess ParseProcessLine(string line) {
            var parts = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var pro in parts) {
                if (pro.Contains('=')) {
                    var kv = pro.Split(new[] { '=' }, 2);
                    dict[kv[0].Trim()] = kv[1].Trim();
                }
                else {
                    if (!dict.ContainsKey("name")) dict["name"] = pro;
                }
            }
            int burst = 10;
            int pri = rnd.Next(1, 6);
            int threadsCount = 1;
            int arrival = tickCount;
            string cwd = "/home/user";
            if (dict.ContainsKey("burst")) int.TryParse(dict["burst"], out burst);
            if (dict.ContainsKey("priority")) int.TryParse(dict["priority"], out pri);
            if (dict.ContainsKey("threads")) int.TryParse(dict["threads"], out threadsCount);
            if (dict.ContainsKey("arrival")) int.TryParse(dict["arrival"], out arrival);
            if (dict.ContainsKey("cwd")) cwd = dict["cwd"];
            var p = CreateProcess(burst, pri, threadsCount, arrival);
            foreach (var t in p.Threads) {
                t.Cwd = cwd;
                if (dict.ContainsKey("files")) {
                    var fls = dict["files"].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
                    foreach (var f in fls) {
                        string path = f.StartsWith("/") ? f : (cwd.TrimEnd('/') + "/" + f);
                        var file = GetFileOrCreate(path);
                        t.OwnedFiles.Add(file);
                    }
                }
                else {
                    if (rnd.NextDouble() < 0.5) {
                        var name = $"auto_{t.ParentPID}_{t.TID}.dat";
                        var file = CreateFile(cwd.TrimEnd('/') + "/" + name, rnd.Next(1, 200));
                        t.OwnedFiles.Add(file);
                    }
                }
            }
            return p;
        }

        private VFile GetFileOrCreate(string path) {
            var p = path.Replace("\\", "/");
            var idx = p.LastIndexOf('/');
            string dir = "/";
            string name = p;
            if (idx >= 0) {
                dir = p.Substring(0, idx);
                name = p.Substring(idx + 1);
            }
            var d = EnsureDirectory(dir == "" ? "/" : dir);
            var f = d.Files.FirstOrDefault(x => x.Name == name);
            if (f == null) {
                f = new VFile { Name = name, Size = rnd.Next(1, 500), Modified = DateTime.Now, Path = (dir == "" ? "/" : dir) + "/" + name };
                d.Files.Add(f);
                RefreshTreeViewFromFs();
            }
            return f;
        }

        private void GenerateSampleProcesses() {
            for (int i = 0; i < 6; i++) {
                int burst = rnd.Next(5, 40);
                int pri = rnd.Next(1, 6);
                int threads = rnd.Next(1, 3);
                var p = CreateProcess(burst, pri, threads, tickCount);
                foreach (var t in p.Threads) {
                    if (rnd.NextDouble() < 0.6) {
                        var f = CreateFile($"/home/user/sample_{p.PID}_{t.TID}.txt", rnd.Next(1, 200));
                        t.OwnedFiles.Add(f);
                    }
                    processos.Add(p);
                    EnqueueReady(t);
                }
            }
        }

        private SimProcess CreateProcess(int burstPerThread, int priority, int threadsCount, int arrivalTick) {
            var p = new SimProcess { PID = nextPID++, Priority = priority, ArrivalTick = arrivalTick };
            for (int i = 0; i < threadsCount; i++) {
                var t = new SimThread {
                    TID = nextTID++,
                    ParentPID = p.PID,
                    TotalBurst = Math.Max(1, burstPerThread),
                    Remaining = Math.Max(1, burstPerThread),
                    PC = 0,
                    State = ThreadState.New,
                    Priority = priority,
                    Cwd = "/home/user"
                };
                int pagesNeeded = Math.Max(1, (int)Math.Ceiling((double)t.TotalBurst / 8.0));
                for (int pg = 0; pg < pagesNeeded; pg++) t.Pages.Add(pg);
                TryAllocatePages(t);
                p.Threads.Add(t);
            }
            return p;
        }

        private void TryAllocatePages(SimThread t) {
            var freeFrames = frames.Where(f => f.Estado == "Livre").ToList();
            if (freeFrames.Count >= t.Pages.Count) {
                for (int i = 0; i < t.Pages.Count; i++) {
                    var f = freeFrames[i];
                    f.Owner = $"P{t.ParentPID}:T{t.TID}";
                    f.Page = t.Pages[i];
                    f.Estado = "Alocada";
                    f.LastUsed = DateTime.Now;
                }
            }
            else {
                int need = t.Pages.Count - freeFrames.Count;
                for (int k = 0; k < need; k++) {
                    var victim = frames.Where(fr => fr.Estado == "Alocada").OrderBy(fr => fr.LastUsed).FirstOrDefault();
                    if (victim != null) {
                        var victimOwner = victim.Owner;
                        victim.Owner = "-";
                        victim.Page = -1;
                        victim.Estado = "Livre";
                        victim.LastUsed = DateTime.MinValue;
                        Log($"Swap out (simulado) frame {victim.FrameIndex} de {victimOwner}");
                    }
                    else {
                        t.State = ThreadState.Waiting;
                        t.WaitingForIOTicks = ioDeviceTicksPerOperation;
                        waitingList.Add(t);
                        ioQueue.Add((t, ioDeviceTicksPerOperation, "AllocIO"));
                        return;
                    }
                }
                var frees = frames.Where(f => f.Estado == "Livre").ToList();
                if (frees.Count >= t.Pages.Count) {
                    for (int i = 0; i < t.Pages.Count; i++) {
                        var f = frees[i];
                        f.Owner = $"P{t.ParentPID}:T{t.TID}";
                        f.Page = t.Pages[i];
                        f.Estado = "Alocada";
                        f.LastUsed = DateTime.Now;
                    }
                }
                else {
                    t.State = ThreadState.Waiting;
                    t.WaitingForIOTicks = ioDeviceTicksPerOperation;
                    waitingList.Add(t);
                    ioQueue.Add((t, ioDeviceTicksPerOperation, "AllocIO"));
                }
            }
        }

        private void EnqueueReady(SimThread t) {
            if (t.State == ThreadState.New || t.State == ThreadState.Waiting) t.State = ThreadState.Ready;
            if (!readyQueue.Contains(t) && t.State == ThreadState.Ready) readyQueue.Enqueue(t);
        }

        private void buttonSTART_Click(object sender, EventArgs e) {
            scheduler = (SchedulerType)comboBoxEscalonador.SelectedIndex;
            running = true;
            timerSO.Start();
            Log("Simulação START");
            UpdateButtons();
        }

        private void buttonSTOP_Click(object sender, EventArgs e) {
            running = false;
            timerSO.Stop();
            Log("Simulação STOP");
            UpdateButtons();
        }

        private void buttonSTEP_Click(object sender, EventArgs e) {
            scheduler = (SchedulerType)comboBoxEscalonador.SelectedIndex;
            Tick();
            UpdateAllViews();
        }

        private void buttonRESET_Click(object sender, EventArgs e) {
            timerSO.Stop();
            ResetSimulationInternal();
            UpdateAllViews();
            Log("Simulação RESET");
            UpdateButtons();
        }

        private void ResetSimulationInternal() {
            nextPID = 1;
            nextTID = 1;
            processos.Clear();
            readyQueue.Clear();
            waitingList.Clear();
            finishedThreads.Clear();
            runningThread = null;
            ioQueue.Clear();
            InitializeMemory();
            InitializeFilesystem();
            tickCount = 0;
            ticksCpuInUse = 0;
            running = false;
            ownerColor.Clear();
            propertyGridProcessos.SelectedObject = null;
        }

        private void timerSO_Tick(object sender, EventArgs e) {
            Tick();
            UpdateAllViews();
        }

        private void Tick() {
            tickCount++;
            ProcessIO();
            ScheduleIfNeeded();
            ExecuteCpuTick();
            UpdateMetricsData();
            RefreshMemoryView();
            UpdateButtons();
        }

        private void ProcessIO() {
            for (int i = ioQueue.Count - 1; i >= 0; i--) {
                var item = ioQueue[i];
                var remaining = item.remaining - 1;
                if (remaining <= 0) {
                    var t = item.thread;
                    if (item.reason == "SwapIn" || item.reason == "AllocIO" || item.reason == "PageFault") {
                        var frees = frames.Where(f => f.Estado == "Livre").ToList();
                        for (int j = 0; j < t.Pages.Count && j < frees.Count; j++) {
                            var f = frees[j];
                            f.Owner = $"P{t.ParentPID}:T{t.TID}";
                            f.Page = t.Pages[j];
                            f.Estado = "Alocada";
                            f.LastUsed = DateTime.Now;
                        }
                    }
                    if (t.State != ThreadState.Terminated) {
                        t.State = ThreadState.Ready;
                        EnqueueReady(t);
                    }
                    waitingList.Remove(t);
                    ioQueue.RemoveAt(i);
                    Log($"I/O ({item.reason}) concluído para P{t.ParentPID}:T{t.TID}");
                }
                else {
                    ioQueue[i] = (item.thread, remaining, item.reason);
                }
            }
        }

        private void ScheduleIfNeeded() {
            if (runningThread == null) {
                PickNextThread();
                return;
            }
            if (scheduler == SchedulerType.RoundRobin) {
                if (runningThread.QuantumUsed >= quantum) {
                    runningThread.QuantumUsed = 0;
                    runningThread.State = ThreadState.Ready;
                    EnqueueReady(runningThread);
                    runningThread = null;
                    PickNextThread();
                }
            }
            else if (scheduler == SchedulerType.Prioridade) {
                var better = readyQueue.FirstOrDefault(r => r.Priority < runningThread.Priority);
                if (better != null) {
                    runningThread.State = ThreadState.Ready;
                    EnqueueReady(runningThread);
                    runningThread = null;
                    PickNextThread();
                }
            }
        }

        private void PickNextThread() {
            if (readyQueue.Count == 0) return;
            if (scheduler == SchedulerType.FCFS) {
                var next = readyQueue.Dequeue();
                runningThread = next;
                runningThread.State = ThreadState.Running;
            }
            else if (scheduler == SchedulerType.RoundRobin) {
                var next = readyQueue.Dequeue();
                runningThread = next;
                runningThread.State = ThreadState.Running;
            }
            else if (scheduler == SchedulerType.Prioridade) {
                var list = readyQueue.ToList();
                list = list.OrderBy(t => t.Priority).ThenBy(t => t.ParentPID).ToList();
                readyQueue = new Queue<SimThread>(list);
                runningThread = readyQueue.Dequeue();
                runningThread.State = ThreadState.Running;
            }
            UpdatePropertyGridForRunningProcess();
        }

        private void ExecuteCpuTick() {
            if (runningThread == null) {
                labelStatusCPU.Text = "Status CPU: Ociosa";
                UpdatePropertyGridForRunningProcess();
                return;
            }
            runningThread.Remaining--;
            runningThread.PC++;
            runningThread.QuantumUsed++;
            runningThread.LastCpuUse = DateTime.Now;
            ticksCpuInUse++;
            labelStatusCPU.Text = $"Status CPU: Executando P{runningThread.ParentPID}:T{runningThread.TID}";
            var ownerTag = $"P{runningThread.ParentPID}:T{runningThread.TID}";
            var usedFrame = frames.FirstOrDefault(f => f.Owner == ownerTag && f.Page == runningThread.Pages[Math.Min(runningThread.CurrentPageIndex, runningThread.Pages.Count - 1)]);
            if (usedFrame != null) usedFrame.LastUsed = DateTime.Now;
            if (runningThread.Remaining <= 0) {
                runningThread.State = ThreadState.Terminated;
                finishedThreads.Add(runningThread);
                FreeFramesOfThread(runningThread);
                Log($"Thread finalizada P{runningThread.ParentPID}:T{runningThread.TID}");
                runningThread = null;
                UpdatePropertyGridForRunningProcess();
            }
            else {
                if (runningThread.PC % 8 == 0) {
                    runningThread.CurrentPageIndex = Math.Min(runningThread.Pages.Count - 1, runningThread.CurrentPageIndex + 1);
                    var pageFound = frames.FirstOrDefault(f => f.Owner == ownerTag && f.Page == runningThread.Pages[runningThread.CurrentPageIndex]);
                    if (pageFound == null) {
                        runningThread.State = ThreadState.Waiting;
                        runningThread.WaitingForIOTicks = ioDeviceTicksPerOperation;
                        waitingList.Add(runningThread);
                        ioQueue.Add((runningThread, ioDeviceTicksPerOperation, "PageFault"));
                        Log($"Page fault P{runningThread.ParentPID}:T{runningThread.TID} page {runningThread.CurrentPageIndex}. I/O requerido");
                        runningThread = null;
                        UpdatePropertyGridForRunningProcess();
                        return;
                    }
                }
                if (rnd.NextDouble() < 0.06 && runningThread.OwnedFiles.Count > 0) {
                    var file = runningThread.OwnedFiles[rnd.Next(runningThread.OwnedFiles.Count)];
                    runningThread.State = ThreadState.Waiting;
                    waitingList.Add(runningThread);
                    ioQueue.Add((runningThread, ioDeviceTicksPerOperation + rnd.Next(0, 4), "FileIO"));
                    file.Modified = DateTime.Now;
                    file.Size += rnd.Next(0, 50);
                    Log($"Operação I/O solicitada por P{runningThread.ParentPID}:T{runningThread.TID} em {file.Path}");
                    runningThread = null;
                    UpdatePropertyGridForRunningProcess();
                }
            }
        }

        private void FreeFramesOfThread(SimThread t) {
            for (int i = 0; i < frames.Count; i++) {
                if (frames[i].Owner == $"P{t.ParentPID}:T{t.TID}") {
                    frames[i].Owner = "-";
                    frames[i].Page = -1;
                    frames[i].Estado = "Livre";
                    frames[i].LastUsed = DateTime.MinValue;
                }
            }
        }

        private void UpdateAllViews() {
            UpdateProcessesGrid();
            UpdateThreadsGrid();
            UpdateMemoryGrid();
            UpdateReadyList();
            UpdateMetricsLabel();
            UpdateMetricsTable();
            UpdateLogsBox();
            RefreshTreeViewFromFs();
            RefreshMemoryView();
            UpdatePropertyGridForRunningProcess();
            UpdateIODevicesView();
        }

        private void UpdateProcessesGrid() {
            dataGridViewProcessos.Rows.Clear();
            foreach (var p in processos) {
                var estado = p.IsFinished ? "Finalizado" : "Ativo";
                dataGridViewProcessos.Rows.Add(p.PID, estado, p.Priority, "-", p.Threads.Sum(t => t.Remaining));
            }
        }

        private void UpdateThreadsGrid() {
            dataGridViewThreads.Rows.Clear();
            foreach (var p in processos) {
                foreach (var t in p.Threads) {
                    dataGridViewThreads.Rows.Add(t.TID, t.ParentPID, t.State.ToString(), t.Remaining);
                }
            }
        }

        private void UpdateMemoryGrid() {
            dataGridViewTabelaMemoria.Rows.Clear();
            foreach (var f in frames) {
                dataGridViewTabelaMemoria.Rows.Add(f.FrameIndex, f.Owner, f.Page >= 0 ? f.Page.ToString() : "-", f.Estado);
            }
        }

        private void UpdateReadyList() {
            listBoxProcessosProntos.Items.Clear();
            foreach (var t in readyQueue) {
                listBoxProcessosProntos.Items.Add($"P{t.ParentPID}:T{t.TID} Pri:{t.Priority} Rem:{t.Remaining}");
            }
        }

        private void UpdateMetricsLabel() {
            int totalProcesses = processos.Count;
            int finished = processos.Count(p => p.IsFinished);
            int readyCount = readyQueue.Count;
            double utilization = tickCount == 0 ? 0.0 : (double)ticksCpuInUse / tickCount * 100.0;
            labelMetricas.Text = $"Ticks totais: {tickCount}\r\nTicks CPU em uso: {ticksCpuInUse}\r\nUtilização CPU: {utilization:F1}%\r\nProcessos: {totalProcesses}\r\nFinalizados: {finished}\r\nFila prontos: {readyCount}";
        }

        private void UpdateMetricsTable() {
            dataGridViewMetricas.Rows.Clear();
            int totalThreads = processos.Sum(p => p.Threads.Count);
            int terminatedThreads = finishedThreads.Count;
            int waiting = waitingList.Count;
            dataGridViewMetricas.Rows.Add("Total processos", processos.Count);
            dataGridViewMetricas.Rows.Add("Total threads", totalThreads);
            dataGridViewMetricas.Rows.Add("Threads finalizadas", terminatedThreads);
            dataGridViewMetricas.Rows.Add("Threads esperando I/O", waiting);
            dataGridViewMetricas.Rows.Add("Frames livres", frames.Count(f => f.Estado == "Livre"));
        }

        private void UpdateLogsBox() {
        }

        private void UpdateFileSystemView() {
            RefreshTreeViewFromFs();
        }

        private void UpdateMetricsData() {
        }

        private void Log(string text) {
            var stamp = $"[{DateTime.Now:HH:mm:ss}] ";
            richTextBoxLogs.AppendText(stamp + text + Environment.NewLine);
            richTextBoxLogs.SelectionStart = richTextBoxLogs.Text.Length;
            richTextBoxLogs.ScrollToCaret();
        }

        private void UpdateButtons() {
            buttonSTART.Enabled = !running;
            buttonSTOP.Enabled = running;
            buttonSTEP.Enabled = !running;
            buttonRESET.Enabled = !running;
            buttonCarregarArquivo.Enabled = !running;
            comboBoxEscalonador.Enabled = !running;
            numericUpDownValorQuantum.Enabled = !running;
        }

        private void DataGridViewProcessos_SelectionChanged(object sender, EventArgs e) {
            if (dataGridViewProcessos.SelectedRows.Count > 0) {
                int pid;
                if (int.TryParse(dataGridViewProcessos.SelectedRows[0].Cells[0].Value?.ToString(), out pid)) {
                    var p = processos.FirstOrDefault(x => x.PID == pid);
                    if (p != null) propertyGridProcessos.SelectedObject = p;
                }
            }
        }

        private void TabControlTabelasInformacoes_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateAllViews();
        }

        private void UpdatePropertyGridForRunningProcess() {
            if (runningThread != null) {
                var proc = processos.FirstOrDefault(p => p.PID == runningThread.ParentPID);
                if (proc != null) {
                    propertyGridProcessos.SelectedObject = proc;
                    return;
                }
            }
            if (propertyGridProcessos.SelectedObject == null && processos.Count > 0) {
                propertyGridProcessos.SelectedObject = processos.First();
            }
        }

        private void PanelVisualizacaoMemoria_Paint(object sender, PaintEventArgs e) {
            var g = e.Graphics;
            g.Clear(panelVisualizacaoMemoria.BackColor);
            int w = Math.Max(1, panelVisualizacaoMemoria.ClientSize.Width);
            int h = Math.Max(1, panelVisualizacaoMemoria.ClientSize.Height);
            int approxCols = Math.Max(1, w / MinCellSize);
            int cols = Math.Min(approxCols, totalFrames);
            if (cols <= 0) cols = 1;
            int rows = (int)Math.Ceiling((double)totalFrames / cols);
            int cellW = Math.Max(4, w / cols);
            int cellH = Math.Max(4, h / rows);
            int idx = 0;
            for (int r = 0; r < rows; r++) {
                for (int c = 0; c < cols; c++) {
                    if (idx >= totalFrames) break;
                    var f = frames[idx];
                    int x = c * cellW;
                    int y = r * cellH;
                    Rectangle rect = new Rectangle(x + 1, y + 1, cellW - 2, cellH - 2);
                    Color color;
                    if (f.Estado == "Livre" || f.Owner == "-") color = Color.FromArgb(90, 90, 90);
                    else {
                        if (!ownerColor.TryGetValue(f.Owner, out color)) {
                            color = Color.FromArgb(rnd.Next(40, 220), rnd.Next(40, 220), rnd.Next(40, 220));
                            ownerColor[f.Owner] = color;
                        }
                        double ageSecs = (DateTime.Now - f.LastUsed).TotalSeconds;
                        float factor = (float)Math.Max(0.35, 1.0 - Math.Min(0.9, ageSecs / 20.0));
                        color = Color.FromArgb(
                            Math.Max(0, (int)(color.R * factor)),
                            Math.Max(0, (int)(color.G * factor)),
                            Math.Max(0, (int)(color.B * factor))
                        );
                    }
                    using (var brush = new SolidBrush(color)) g.FillRectangle(brush, rect);
                    using (var pen = new Pen(Color.FromArgb(40, 40, 40))) g.DrawRectangle(pen, rect);
                    if (cellW > 30 && cellH > 14) {
                        string text = f.Owner == "-" ? "Livre" : $"{f.Owner}\nPg:{(f.Page >= 0 ? f.Page.ToString() : "-")}";
                        var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        var font = new Font("Segoe UI", Math.Max(6, Math.Min(10, cellH / 4)));
                        g.DrawString(text, font, Brushes.White, rect, sf);
                        font.Dispose();
                    }
                    idx++;
                }
            }
        }

        private void RefreshMemoryView() {
            panelVisualizacaoMemoria.Invalidate();
        }

        private void InitializeIODevicesView() {
            dataGridViewIODevices = new DataGridView();
            dataGridViewIODevices.Dock = DockStyle.Fill;
            dataGridViewIODevices.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewIODevices.EnableHeadersVisualStyles = false;
            dataGridViewIODevices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            dataGridViewIODevices.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewIODevices.DefaultCellStyle.BackColor = Color.FromArgb(34, 34, 34);
            dataGridViewIODevices.DefaultCellStyle.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewIODevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewIODevices.RowHeadersVisible = false;
            dataGridViewIODevices.Columns.Add("Device", "Dispositivo");
            dataGridViewIODevices.Columns.Add("Owner", "Owner (P:T)");
            dataGridViewIODevices.Columns.Add("Operation", "Operação");
            dataGridViewIODevices.Columns.Add("Target", "Destino");
            dataGridViewIODevices.Columns.Add("Remaining", "Ticks restantes");
            dataGridViewIODevices.Columns.Add("State", "Estado Thread");
            tabPageDispositivosIO.Controls.Clear();
            tabPageDispositivosIO.Controls.Add(dataGridViewIODevices);
        }

        private void UpdateIODevicesView() {
            if (dataGridViewIODevices == null) return;
            dataGridViewIODevices.Rows.Clear();
            foreach (var item in ioQueue) {
                var t = item.thread;
                string owner = $"P{t.ParentPID}:T{t.TID}";
                string device = MapReasonToDevice(item.reason);
                string operation = item.reason;
                string target = "-";
                if (item.reason == "FileIO") {
                    if (t.OwnedFiles.Count > 0) target = t.OwnedFiles.First().Path;
                }
                else if (item.reason == "PageFault") {
                    int pageIdx = Math.Min(t.CurrentPageIndex, t.Pages.Count - 1);
                    target = $"Page {pageIdx}";
                }
                else if (item.reason == "SwapIn" || item.reason == "AllocIO") {
                    target = "Memória/Swap";
                }
                else {
                    target = item.reason;
                }
                string state = t.State.ToString();
                dataGridViewIODevices.Rows.Add(device, owner, operation, target, item.remaining, state);
            }
            foreach (var t in waitingList) {
                bool inQueue = ioQueue.Any(i => i.thread == t);
                if (!inQueue) {
                    string owner = $"P{t.ParentPID}:T{t.TID}";
                    string device = "I/O (aguardando)";
                    string operation = "Waiting";
                    string target = t.OwnedFiles.Count > 0 ? t.OwnedFiles.First().Path : "page/alloc";
                    string state = t.State.ToString();
                    dataGridViewIODevices.Rows.Add(device, owner, operation, target, t.WaitingForIOTicks, state);
                }
            }
            if (runningThread != null) {
                var rt = runningThread;
                string owner = $"P{rt.ParentPID}:T{rt.TID}";
                string device = "CPU";
                string operation = "Executing";
                string target = rt.OwnedFiles.Count > 0 ? rt.OwnedFiles.First().Path : "-";
                string state = rt.State.ToString();
                dataGridViewIODevices.Rows.Insert(0, device, owner, operation, target, "-", state);
            }
        }

        private string MapReasonToDevice(string reason) {
            if (string.IsNullOrEmpty(reason)) return "Unknown";
            var r = reason.ToLowerInvariant();
            if (r.Contains("file")) return "Disco/FS";
            if (r.Contains("page") || r.Contains("swap") || r.Contains("alloc")) return "Memória/Swap";
            return "Dispositivo";
        }
    }
}

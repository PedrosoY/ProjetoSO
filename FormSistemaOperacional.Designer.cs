namespace ProjetoSOFinal
{
    partial class FormSistemaOperacional
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            panelControles = new Panel();
            buttonRESET = new Button();
            comboBoxEscalonador = new ComboBox();
            buttonSTEP = new Button();
            buttonSTOP = new Button();
            buttonSTART = new Button();
            buttonCarregarArquivo = new Button();
            numericUpDownValorQuantum = new NumericUpDown();
            labelQuantum = new Label();
            labelEscalonador = new Label();
            panelEsquerda = new Panel();
            listBoxProcessosProntos = new ListBox();
            labelMetricas = new Label();
            labelStatusCPU = new Label();
            panelTabelas = new Panel();
            tabControlTabelasInformacoes = new TabControl();
            tabPageProcessos = new TabPage();
            dataGridViewProcessos = new DataGridView();
            ProcessoColunaPID = new DataGridViewTextBoxColumn();
            ProcessoColunaEstado = new DataGridViewTextBoxColumn();
            ProcessoColunaPrioridade = new DataGridViewTextBoxColumn();
            ProcessoColunaPC = new DataGridViewTextBoxColumn();
            ProcessoColunaRestante = new DataGridViewTextBoxColumn();
            propertyGridProcessos = new PropertyGrid();
            tabPageThreads = new TabPage();
            dataGridViewThreads = new DataGridView();
            ThreadColunaTID = new DataGridViewTextBoxColumn();
            ThreadColunaProcessoPai = new DataGridViewTextBoxColumn();
            ThreadColunaEstado = new DataGridViewTextBoxColumn();
            ThreadColunaRestante = new DataGridViewTextBoxColumn();
            tabPageMemoria = new TabPage();
            dataGridViewTabelaMemoria = new DataGridView();
            MemoriaColunaFrame = new DataGridViewTextBoxColumn();
            MemoriaColunaThread_PID = new DataGridViewTextBoxColumn();
            MemoriaColunaPagina = new DataGridViewTextBoxColumn();
            MemoriaColunaEstado = new DataGridViewTextBoxColumn();
            panelVisualizacaoMemoria = new Panel();
            tabPageDispositivosIO = new TabPage();
            tabPageSistemaArquivos = new TabPage();
            treeViewSistemaArquivos = new TreeView();
            propertyGridArquivo = new PropertyGrid();
            tabPageMetricas = new TabPage();
            dataGridViewMetricas = new DataGridView();
            MetricaColunaMetrica = new DataGridViewTextBoxColumn();
            MetricaColunaValorMetrica = new DataGridViewTextBoxColumn();
            panelLogs = new Panel();
            richTextBoxLogs = new RichTextBox();
            timerSO = new System.Windows.Forms.Timer(components);
            panelControles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownValorQuantum).BeginInit();
            panelEsquerda.SuspendLayout();
            panelTabelas.SuspendLayout();
            tabControlTabelasInformacoes.SuspendLayout();
            tabPageProcessos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewProcessos).BeginInit();
            tabPageThreads.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewThreads).BeginInit();
            tabPageMemoria.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewTabelaMemoria).BeginInit();
            tabPageSistemaArquivos.SuspendLayout();
            tabPageMetricas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewMetricas).BeginInit();
            panelLogs.SuspendLayout();
            SuspendLayout();
            // 
            // panelControles
            // 
            panelControles.BackColor = Color.FromArgb(30, 30, 30);
            panelControles.Controls.Add(buttonRESET);
            panelControles.Controls.Add(comboBoxEscalonador);
            panelControles.Controls.Add(buttonSTEP);
            panelControles.Controls.Add(buttonSTOP);
            panelControles.Controls.Add(buttonSTART);
            panelControles.Controls.Add(buttonCarregarArquivo);
            panelControles.Controls.Add(numericUpDownValorQuantum);
            panelControles.Controls.Add(labelQuantum);
            panelControles.Controls.Add(labelEscalonador);
            panelControles.Dock = DockStyle.Top;
            panelControles.Location = new Point(220, 0);
            panelControles.Name = "panelControles";
            panelControles.Size = new Size(1014, 94);
            panelControles.TabIndex = 0;
            // 
            // buttonRESET
            // 
            buttonRESET.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonRESET.BackColor = Color.Purple;
            buttonRESET.FlatAppearance.BorderSize = 0;
            buttonRESET.FlatStyle = FlatStyle.Flat;
            buttonRESET.ForeColor = Color.White;
            buttonRESET.Location = new Point(448, 20);
            buttonRESET.Name = "buttonRESET";
            buttonRESET.Size = new Size(136, 52);
            buttonRESET.TabIndex = 8;
            buttonRESET.Text = "RESET";
            buttonRESET.UseVisualStyleBackColor = false;
            buttonRESET.Click += buttonRESET_Click;
            // 
            // comboBoxEscalonador
            // 
            comboBoxEscalonador.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBoxEscalonador.BackColor = Color.FromArgb(34, 34, 34);
            comboBoxEscalonador.DisplayMember = "dfsdfsf";
            comboBoxEscalonador.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxEscalonador.FlatStyle = FlatStyle.Flat;
            comboBoxEscalonador.ForeColor = Color.FromArgb(230, 230, 230);
            comboBoxEscalonador.FormattingEnabled = true;
            comboBoxEscalonador.Items.AddRange(new object[] { "Round Robin", "FCFS", "Pripridade" });
            comboBoxEscalonador.Location = new Point(86, 20);
            comboBoxEscalonador.Name = "comboBoxEscalonador";
            comboBoxEscalonador.Size = new Size(205, 23);
            comboBoxEscalonador.TabIndex = 1;
            // 
            // buttonSTEP
            // 
            buttonSTEP.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            buttonSTEP.BackColor = Color.Green;
            buttonSTEP.FlatAppearance.BorderSize = 0;
            buttonSTEP.FlatStyle = FlatStyle.Flat;
            buttonSTEP.ForeColor = Color.FromArgb(230, 230, 230);
            buttonSTEP.Location = new Point(874, 20);
            buttonSTEP.Name = "buttonSTEP";
            buttonSTEP.Size = new Size(136, 52);
            buttonSTEP.TabIndex = 7;
            buttonSTEP.Text = "STEP";
            buttonSTEP.UseVisualStyleBackColor = false;
            buttonSTEP.Click += buttonSTEP_Click;
            // 
            // buttonSTOP
            // 
            buttonSTOP.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSTOP.BackColor = Color.FromArgb(180, 40, 40);
            buttonSTOP.FlatAppearance.BorderSize = 0;
            buttonSTOP.FlatStyle = FlatStyle.Flat;
            buttonSTOP.ForeColor = Color.FromArgb(230, 230, 230);
            buttonSTOP.Location = new Point(732, 20);
            buttonSTOP.Name = "buttonSTOP";
            buttonSTOP.Size = new Size(136, 52);
            buttonSTOP.TabIndex = 6;
            buttonSTOP.Text = "STOP";
            buttonSTOP.UseVisualStyleBackColor = false;
            buttonSTOP.Click += buttonSTOP_Click;
            // 
            // buttonSTART
            // 
            buttonSTART.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSTART.AutoSize = true;
            buttonSTART.BackColor = Color.FromArgb(0, 122, 204);
            buttonSTART.FlatAppearance.BorderSize = 0;
            buttonSTART.FlatStyle = FlatStyle.Flat;
            buttonSTART.ForeColor = Color.White;
            buttonSTART.Location = new Point(590, 20);
            buttonSTART.Name = "buttonSTART";
            buttonSTART.Size = new Size(136, 52);
            buttonSTART.TabIndex = 5;
            buttonSTART.Text = "START";
            buttonSTART.UseVisualStyleBackColor = false;
            buttonSTART.Click += buttonSTART_Click;
            // 
            // buttonCarregarArquivo
            // 
            buttonCarregarArquivo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCarregarArquivo.BackColor = Color.FromArgb(40, 40, 40);
            buttonCarregarArquivo.FlatAppearance.BorderSize = 0;
            buttonCarregarArquivo.FlatStyle = FlatStyle.Flat;
            buttonCarregarArquivo.ForeColor = Color.FromArgb(230, 230, 230);
            buttonCarregarArquivo.Location = new Point(306, 20);
            buttonCarregarArquivo.Name = "buttonCarregarArquivo";
            buttonCarregarArquivo.Size = new Size(136, 52);
            buttonCarregarArquivo.TabIndex = 4;
            buttonCarregarArquivo.Text = "Carregar Processos";
            buttonCarregarArquivo.UseVisualStyleBackColor = false;
            buttonCarregarArquivo.Click += buttonCarregarArquivo_Click;
            // 
            // numericUpDownValorQuantum
            // 
            numericUpDownValorQuantum.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            numericUpDownValorQuantum.BackColor = Color.FromArgb(34, 34, 34);
            numericUpDownValorQuantum.ForeColor = Color.FromArgb(230, 230, 230);
            numericUpDownValorQuantum.Location = new Point(86, 49);
            numericUpDownValorQuantum.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownValorQuantum.Name = "numericUpDownValorQuantum";
            numericUpDownValorQuantum.Size = new Size(205, 23);
            numericUpDownValorQuantum.TabIndex = 3;
            numericUpDownValorQuantum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // labelQuantum
            // 
            labelQuantum.AutoSize = true;
            labelQuantum.ForeColor = Color.FromArgb(230, 230, 230);
            labelQuantum.Location = new Point(19, 49);
            labelQuantum.Name = "labelQuantum";
            labelQuantum.Size = new Size(61, 15);
            labelQuantum.TabIndex = 2;
            labelQuantum.Text = "Quantum:";
            // 
            // labelEscalonador
            // 
            labelEscalonador.AutoSize = true;
            labelEscalonador.ForeColor = Color.FromArgb(230, 230, 230);
            labelEscalonador.Location = new Point(6, 20);
            labelEscalonador.Name = "labelEscalonador";
            labelEscalonador.Size = new Size(74, 15);
            labelEscalonador.TabIndex = 0;
            labelEscalonador.Text = "Escalonador:";
            // 
            // panelEsquerda
            // 
            panelEsquerda.BackColor = Color.FromArgb(24, 24, 24);
            panelEsquerda.Controls.Add(listBoxProcessosProntos);
            panelEsquerda.Controls.Add(labelMetricas);
            panelEsquerda.Controls.Add(labelStatusCPU);
            panelEsquerda.Dock = DockStyle.Left;
            panelEsquerda.Location = new Point(0, 0);
            panelEsquerda.Name = "panelEsquerda";
            panelEsquerda.Size = new Size(220, 701);
            panelEsquerda.TabIndex = 1;
            // 
            // listBoxProcessosProntos
            // 
            listBoxProcessosProntos.BackColor = Color.FromArgb(34, 34, 34);
            listBoxProcessosProntos.Dock = DockStyle.Fill;
            listBoxProcessosProntos.ForeColor = Color.FromArgb(230, 230, 230);
            listBoxProcessosProntos.FormattingEnabled = true;
            listBoxProcessosProntos.Location = new Point(0, 28);
            listBoxProcessosProntos.Name = "listBoxProcessosProntos";
            listBoxProcessosProntos.Size = new Size(220, 371);
            listBoxProcessosProntos.TabIndex = 2;
            // 
            // labelMetricas
            // 
            labelMetricas.BackColor = Color.Transparent;
            labelMetricas.Dock = DockStyle.Bottom;
            labelMetricas.Font = new Font("Segoe UI", 10F);
            labelMetricas.ForeColor = Color.FromArgb(230, 230, 230);
            labelMetricas.Location = new Point(0, 399);
            labelMetricas.Name = "labelMetricas";
            labelMetricas.Padding = new Padding(8);
            labelMetricas.Size = new Size(220, 302);
            labelMetricas.TabIndex = 3;
            labelMetricas.Text = "Ticks totais: 0\r\nTicks CPU em uso: 0\r\nUtilização CPU: 0.0%\r\nProcessos: 0\r\nFinalizados: 0\r\nFila prontos: 0";
            // 
            // labelStatusCPU
            // 
            labelStatusCPU.BackColor = Color.Transparent;
            labelStatusCPU.Dock = DockStyle.Top;
            labelStatusCPU.ForeColor = Color.FromArgb(230, 230, 230);
            labelStatusCPU.Location = new Point(0, 0);
            labelStatusCPU.Name = "labelStatusCPU";
            labelStatusCPU.Padding = new Padding(8, 0, 0, 0);
            labelStatusCPU.Size = new Size(220, 28);
            labelStatusCPU.TabIndex = 4;
            labelStatusCPU.Text = "Status CPU: Ociosa";
            labelStatusCPU.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelTabelas
            // 
            panelTabelas.BackColor = Color.FromArgb(30, 30, 30);
            panelTabelas.Controls.Add(tabControlTabelasInformacoes);
            panelTabelas.Dock = DockStyle.Fill;
            panelTabelas.Location = new Point(220, 94);
            panelTabelas.Name = "panelTabelas";
            panelTabelas.Size = new Size(1014, 432);
            panelTabelas.TabIndex = 2;
            // 
            // tabControlTabelasInformacoes
            // 
            tabControlTabelasInformacoes.Controls.Add(tabPageProcessos);
            tabControlTabelasInformacoes.Controls.Add(tabPageThreads);
            tabControlTabelasInformacoes.Controls.Add(tabPageMemoria);
            tabControlTabelasInformacoes.Controls.Add(tabPageDispositivosIO);
            tabControlTabelasInformacoes.Controls.Add(tabPageSistemaArquivos);
            tabControlTabelasInformacoes.Controls.Add(tabPageMetricas);
            tabControlTabelasInformacoes.Dock = DockStyle.Fill;
            tabControlTabelasInformacoes.Location = new Point(0, 0);
            tabControlTabelasInformacoes.Name = "tabControlTabelasInformacoes";
            tabControlTabelasInformacoes.SelectedIndex = 0;
            tabControlTabelasInformacoes.Size = new Size(1014, 432);
            tabControlTabelasInformacoes.TabIndex = 0;
            // 
            // tabPageProcessos
            // 
            tabPageProcessos.BackColor = Color.FromArgb(30, 30, 30);
            tabPageProcessos.Controls.Add(dataGridViewProcessos);
            tabPageProcessos.Controls.Add(propertyGridProcessos);
            tabPageProcessos.Location = new Point(4, 24);
            tabPageProcessos.Name = "tabPageProcessos";
            tabPageProcessos.Padding = new Padding(3);
            tabPageProcessos.Size = new Size(1006, 404);
            tabPageProcessos.TabIndex = 0;
            tabPageProcessos.Text = "Processos";
            // 
            // dataGridViewProcessos
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(40, 40, 40);
            dataGridViewCellStyle1.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewProcessos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewProcessos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProcessos.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridViewProcessos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewProcessos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewProcessos.Columns.AddRange(new DataGridViewColumn[] { ProcessoColunaPID, ProcessoColunaEstado, ProcessoColunaPrioridade, ProcessoColunaPC, ProcessoColunaRestante });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(34, 34, 34);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridViewProcessos.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewProcessos.Dock = DockStyle.Fill;
            dataGridViewProcessos.EnableHeadersVisualStyles = false;
            dataGridViewProcessos.Location = new Point(3, 3);
            dataGridViewProcessos.Name = "dataGridViewProcessos";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridViewProcessos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewProcessos.Size = new Size(766, 398);
            dataGridViewProcessos.TabIndex = 0;
            // 
            // ProcessoColunaPID
            // 
            ProcessoColunaPID.HeaderText = "PID";
            ProcessoColunaPID.Name = "ProcessoColunaPID";
            // 
            // ProcessoColunaEstado
            // 
            ProcessoColunaEstado.HeaderText = "Estado";
            ProcessoColunaEstado.Name = "ProcessoColunaEstado";
            // 
            // ProcessoColunaPrioridade
            // 
            ProcessoColunaPrioridade.HeaderText = "Prioridade";
            ProcessoColunaPrioridade.Name = "ProcessoColunaPrioridade";
            // 
            // ProcessoColunaPC
            // 
            ProcessoColunaPC.HeaderText = "PC";
            ProcessoColunaPC.Name = "ProcessoColunaPC";
            // 
            // ProcessoColunaRestante
            // 
            ProcessoColunaRestante.HeaderText = "Restante";
            ProcessoColunaRestante.Name = "ProcessoColunaRestante";
            // 
            // propertyGridProcessos
            // 
            propertyGridProcessos.BackColor = Color.FromArgb(34, 34, 34);
            propertyGridProcessos.Dock = DockStyle.Right;
            propertyGridProcessos.ForeColor = Color.FromArgb(230, 230, 230);
            propertyGridProcessos.HelpVisible = false;
            propertyGridProcessos.Location = new Point(769, 3);
            propertyGridProcessos.Name = "propertyGridProcessos";
            propertyGridProcessos.Size = new Size(234, 398);
            propertyGridProcessos.TabIndex = 1;
            propertyGridProcessos.ToolbarVisible = false;
            // 
            // tabPageThreads
            // 
            tabPageThreads.BackColor = Color.FromArgb(30, 30, 30);
            tabPageThreads.Controls.Add(dataGridViewThreads);
            tabPageThreads.Location = new Point(4, 24);
            tabPageThreads.Name = "tabPageThreads";
            tabPageThreads.Padding = new Padding(3);
            tabPageThreads.Size = new Size(1006, 404);
            tabPageThreads.TabIndex = 1;
            tabPageThreads.Text = "Threads";
            // 
            // dataGridViewThreads
            // 
            dataGridViewThreads.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewThreads.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridViewThreads.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewThreads.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewThreads.Columns.AddRange(new DataGridViewColumn[] { ThreadColunaTID, ThreadColunaProcessoPai, ThreadColunaEstado, ThreadColunaRestante });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(34, 34, 34);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dataGridViewThreads.DefaultCellStyle = dataGridViewCellStyle6;
            dataGridViewThreads.Dock = DockStyle.Fill;
            dataGridViewThreads.EnableHeadersVisualStyles = false;
            dataGridViewThreads.Location = new Point(3, 3);
            dataGridViewThreads.Name = "dataGridViewThreads";
            dataGridViewThreads.Size = new Size(1000, 398);
            dataGridViewThreads.TabIndex = 0;
            // 
            // ThreadColunaTID
            // 
            ThreadColunaTID.HeaderText = "TID";
            ThreadColunaTID.Name = "ThreadColunaTID";
            // 
            // ThreadColunaProcessoPai
            // 
            ThreadColunaProcessoPai.HeaderText = "PID PAI";
            ThreadColunaProcessoPai.Name = "ThreadColunaProcessoPai";
            // 
            // ThreadColunaEstado
            // 
            ThreadColunaEstado.HeaderText = "Estado";
            ThreadColunaEstado.Name = "ThreadColunaEstado";
            // 
            // ThreadColunaRestante
            // 
            ThreadColunaRestante.HeaderText = "Restante";
            ThreadColunaRestante.Name = "ThreadColunaRestante";
            // 
            // tabPageMemoria
            // 
            tabPageMemoria.BackColor = Color.FromArgb(30, 30, 30);
            tabPageMemoria.Controls.Add(dataGridViewTabelaMemoria);
            tabPageMemoria.Controls.Add(panelVisualizacaoMemoria);
            tabPageMemoria.Location = new Point(4, 24);
            tabPageMemoria.Name = "tabPageMemoria";
            tabPageMemoria.Size = new Size(1006, 404);
            tabPageMemoria.TabIndex = 2;
            tabPageMemoria.Text = "Memoria";
            // 
            // dataGridViewTabelaMemoria
            // 
            dataGridViewTabelaMemoria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewTabelaMemoria.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle7.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.True;
            dataGridViewTabelaMemoria.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewTabelaMemoria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewTabelaMemoria.Columns.AddRange(new DataGridViewColumn[] { MemoriaColunaFrame, MemoriaColunaThread_PID, MemoriaColunaPagina, MemoriaColunaEstado });
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(34, 34, 34);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle8.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
            dataGridViewTabelaMemoria.DefaultCellStyle = dataGridViewCellStyle8;
            dataGridViewTabelaMemoria.Dock = DockStyle.Bottom;
            dataGridViewTabelaMemoria.EnableHeadersVisualStyles = false;
            dataGridViewTabelaMemoria.Location = new Point(0, 161);
            dataGridViewTabelaMemoria.Name = "dataGridViewTabelaMemoria";
            dataGridViewTabelaMemoria.Size = new Size(1006, 243);
            dataGridViewTabelaMemoria.TabIndex = 0;
            // 
            // MemoriaColunaFrame
            // 
            MemoriaColunaFrame.HeaderText = "Frame";
            MemoriaColunaFrame.Name = "MemoriaColunaFrame";
            // 
            // MemoriaColunaThread_PID
            // 
            MemoriaColunaThread_PID.HeaderText = "PID/Thread";
            MemoriaColunaThread_PID.Name = "MemoriaColunaThread_PID";
            // 
            // MemoriaColunaPagina
            // 
            MemoriaColunaPagina.HeaderText = "Pagina";
            MemoriaColunaPagina.Name = "MemoriaColunaPagina";
            // 
            // MemoriaColunaEstado
            // 
            MemoriaColunaEstado.HeaderText = "Estado";
            MemoriaColunaEstado.Name = "MemoriaColunaEstado";
            // 
            // panelVisualizacaoMemoria
            // 
            panelVisualizacaoMemoria.BackColor = Color.FromArgb(30, 30, 30);
            panelVisualizacaoMemoria.Dock = DockStyle.Fill;
            panelVisualizacaoMemoria.Location = new Point(0, 0);
            panelVisualizacaoMemoria.Name = "panelVisualizacaoMemoria";
            panelVisualizacaoMemoria.Size = new Size(1006, 404);
            panelVisualizacaoMemoria.TabIndex = 1;
            // 
            // tabPageDispositivosIO
            // 
            tabPageDispositivosIO.BackColor = Color.FromArgb(30, 30, 30);
            tabPageDispositivosIO.Location = new Point(4, 24);
            tabPageDispositivosIO.Name = "tabPageDispositivosIO";
            tabPageDispositivosIO.Size = new Size(1006, 404);
            tabPageDispositivosIO.TabIndex = 4;
            tabPageDispositivosIO.Text = "Dispositivos I/O";
            // 
            // tabPageSistemaArquivos
            // 
            tabPageSistemaArquivos.BackColor = Color.FromArgb(30, 30, 30);
            tabPageSistemaArquivos.Controls.Add(treeViewSistemaArquivos);
            tabPageSistemaArquivos.Controls.Add(propertyGridArquivo);
            tabPageSistemaArquivos.Location = new Point(4, 24);
            tabPageSistemaArquivos.Name = "tabPageSistemaArquivos";
            tabPageSistemaArquivos.Size = new Size(1006, 404);
            tabPageSistemaArquivos.TabIndex = 3;
            tabPageSistemaArquivos.Text = "Sistema de Arquivos";
            // 
            // treeViewSistemaArquivos
            // 
            treeViewSistemaArquivos.BackColor = Color.FromArgb(34, 34, 34);
            treeViewSistemaArquivos.Dock = DockStyle.Fill;
            treeViewSistemaArquivos.ForeColor = Color.FromArgb(230, 230, 230);
            treeViewSistemaArquivos.Location = new Point(0, 0);
            treeViewSistemaArquivos.Name = "treeViewSistemaArquivos";
            treeViewSistemaArquivos.Size = new Size(754, 404);
            treeViewSistemaArquivos.TabIndex = 0;
            // 
            // propertyGridArquivo
            // 
            propertyGridArquivo.BackColor = Color.FromArgb(34, 34, 34);
            propertyGridArquivo.Dock = DockStyle.Right;
            propertyGridArquivo.ForeColor = Color.FromArgb(230, 230, 230);
            propertyGridArquivo.HelpVisible = false;
            propertyGridArquivo.Location = new Point(754, 0);
            propertyGridArquivo.Name = "propertyGridArquivo";
            propertyGridArquivo.Size = new Size(252, 404);
            propertyGridArquivo.TabIndex = 1;
            propertyGridArquivo.ToolbarVisible = false;
            // 
            // tabPageMetricas
            // 
            tabPageMetricas.BackColor = Color.FromArgb(30, 30, 30);
            tabPageMetricas.Controls.Add(dataGridViewMetricas);
            tabPageMetricas.Location = new Point(4, 24);
            tabPageMetricas.Name = "tabPageMetricas";
            tabPageMetricas.Size = new Size(1006, 404);
            tabPageMetricas.TabIndex = 5;
            tabPageMetricas.Text = "Metricas";
            // 
            // dataGridViewMetricas
            // 
            dataGridViewMetricas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewMetricas.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewMetricas.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = Color.FromArgb(45, 45, 48);
            dataGridViewCellStyle9.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle9.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = DataGridViewTriState.True;
            dataGridViewMetricas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            dataGridViewMetricas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewMetricas.Columns.AddRange(new DataGridViewColumn[] { MetricaColunaMetrica, MetricaColunaValorMetrica });
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = Color.FromArgb(34, 34, 34);
            dataGridViewCellStyle10.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle10.ForeColor = Color.FromArgb(230, 230, 230);
            dataGridViewCellStyle10.SelectionBackColor = Color.FromArgb(0, 122, 204);
            dataGridViewCellStyle10.SelectionForeColor = Color.White;
            dataGridViewCellStyle10.WrapMode = DataGridViewTriState.False;
            dataGridViewMetricas.DefaultCellStyle = dataGridViewCellStyle10;
            dataGridViewMetricas.Dock = DockStyle.Fill;
            dataGridViewMetricas.EnableHeadersVisualStyles = false;
            dataGridViewMetricas.Location = new Point(0, 0);
            dataGridViewMetricas.Name = "dataGridViewMetricas";
            dataGridViewMetricas.Size = new Size(1006, 404);
            dataGridViewMetricas.TabIndex = 0;
            // 
            // MetricaColunaMetrica
            // 
            MetricaColunaMetrica.HeaderText = "Metrica";
            MetricaColunaMetrica.Name = "MetricaColunaMetrica";
            // 
            // MetricaColunaValorMetrica
            // 
            MetricaColunaValorMetrica.HeaderText = "Valor";
            MetricaColunaValorMetrica.Name = "MetricaColunaValorMetrica";
            // 
            // panelLogs
            // 
            panelLogs.BackColor = Color.FromArgb(30, 30, 30);
            panelLogs.Controls.Add(richTextBoxLogs);
            panelLogs.Dock = DockStyle.Bottom;
            panelLogs.Location = new Point(220, 526);
            panelLogs.Name = "panelLogs";
            panelLogs.Size = new Size(1014, 175);
            panelLogs.TabIndex = 3;
            // 
            // richTextBoxLogs
            // 
            richTextBoxLogs.BackColor = Color.FromArgb(34, 34, 34);
            richTextBoxLogs.BorderStyle = BorderStyle.None;
            richTextBoxLogs.Dock = DockStyle.Fill;
            richTextBoxLogs.ForeColor = Color.FromArgb(230, 230, 230);
            richTextBoxLogs.Location = new Point(0, 0);
            richTextBoxLogs.Name = "richTextBoxLogs";
            richTextBoxLogs.Size = new Size(1014, 175);
            richTextBoxLogs.TabIndex = 0;
            richTextBoxLogs.Text = "";
            // 
            // timerSO
            // 
            timerSO.Tick += timerSO_Tick;
            // 
            // FormSistemaOperacional
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 24, 24);
            ClientSize = new Size(1234, 701);
            Controls.Add(panelTabelas);
            Controls.Add(panelControles);
            Controls.Add(panelLogs);
            Controls.Add(panelEsquerda);
            ForeColor = Color.FromArgb(230, 230, 230);
            Name = "FormSistemaOperacional";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form Sisteminha Brabo";
            WindowState = FormWindowState.Maximized;
            Load += FormSistemaOperacional_Load;
            panelControles.ResumeLayout(false);
            panelControles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownValorQuantum).EndInit();
            panelEsquerda.ResumeLayout(false);
            panelTabelas.ResumeLayout(false);
            tabControlTabelasInformacoes.ResumeLayout(false);
            tabPageProcessos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewProcessos).EndInit();
            tabPageThreads.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewThreads).EndInit();
            tabPageMemoria.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewTabelaMemoria).EndInit();
            tabPageSistemaArquivos.ResumeLayout(false);
            tabPageMetricas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewMetricas).EndInit();
            panelLogs.ResumeLayout(false);
            ResumeLayout(false);
        }


        #endregion

        private Panel panelControles;
        private Panel panelEsquerda;
        private Panel panelTabelas;
        private Label labelQuantum;
        private ComboBox comboBoxEscalonador;
        private Label labelEscalonador;
        private Button buttonSTEP;
        private Button buttonSTOP;
        private Button buttonSTART;
        private Button buttonCarregarArquivo;
        private NumericUpDown numericUpDownValorQuantum;
        private TabControl tabControlTabelasInformacoes;
        private TabPage tabPageProcessos;
        private TabPage tabPageThreads;
        private TabPage tabPageMemoria;
        private TabPage tabPageSistemaArquivos;
        private TabPage tabPageDispositivosIO;
        private TabPage tabPageMetricas;
        private Panel panelLogs;
        private DataGridView dataGridViewProcessos;
        private PropertyGrid propertyGridProcessos;
        private DataGridView dataGridViewThreads;
        private Panel panelVisualizacaoMemoria;
        private DataGridView dataGridViewTabelaMemoria;
        private TreeView treeViewSistemaArquivos;
        private PropertyGrid propertyGridArquivo;
        private DataGridView dataGridViewMetricas;
        private ListBox listBoxProcessosProntos;
        private Label labelMetricas;
        private Label labelStatusCPU;
        private DataGridViewTextBoxColumn ThreadColunaTID;
        private DataGridViewTextBoxColumn ThreadColunaProcessoPai;
        private DataGridViewTextBoxColumn ThreadColunaEstado;
        private DataGridViewTextBoxColumn ThreadColunaRestante;
        private DataGridViewTextBoxColumn MemoriaColunaFrame;
        private DataGridViewTextBoxColumn MemoriaColunaThread_PID;
        private DataGridViewTextBoxColumn MemoriaColunaPagina;
        private DataGridViewTextBoxColumn MemoriaColunaEstado;
        private DataGridViewTextBoxColumn MetricaColunaMetrica;
        private DataGridViewTextBoxColumn MetricaColunaValorMetrica;
        private DataGridViewTextBoxColumn ProcessoColunaPID;
        private DataGridViewTextBoxColumn ProcessoColunaEstado;
        private DataGridViewTextBoxColumn ProcessoColunaPrioridade;
        private DataGridViewTextBoxColumn ProcessoColunaPC;
        private DataGridViewTextBoxColumn ProcessoColunaRestante;
        private RichTextBox richTextBoxLogs;
        private Button buttonRESET;
        private System.Windows.Forms.Timer timerSO;
    }
}

README — ProjetoSOFinal
=======================

1) COMO PEGAR O EXECUTÁVEL (RELEASES)
------------------------------------
1. Acesse a página do repositório (GitHub) do ProjetoSOFinal.
2. Clique na aba **Releases**.
3. Abra a **última release** disponível.
4. Faça o download dos *assets* da release — normalmente haverá um arquivo ZIP com o executável e recursos. Baixe **todos** os arquivos do asset (ou o ZIP contendo tudo).
5. Extraia o ZIP para uma pasta no seu Windows.
6. Execute o arquivo `ProjetoSOFinal.exe` (duplo-clique). O programa é uma aplicação WinForms — requer Windows e runtime .NET compatível (recomendado: .NET 6+ ou .NET Framework 4.8 — foi desemvolvido com .NET 9).

USO RÁPIDO (o que fazer ao abrir o programa)
-------------------------------------------
- Clique em **Carregar Processos** para carregar um arquivo `.txt` com a descrição dos processos (formato explicado abaixo) ou deixe o simulador gerar processos de amostra.
- Escolha o **Escalonador** (Round Robin, FCFS, Prioridade) no combo.
- Ajuste o **Quantum** (usado no Round Robin).
- Use **START** para iniciar a simulação, **STOP** para pausar, **STEP** para avançar um tick, **RESET** para voltar ao estado inicial.
- Veja logs na área de logs; métricas na lateral esquerda e na aba *Métricas*; tabelas de processos/threads/memória/FS nas abas; e o painel visual de memória em *Memoria*.

INPUT: FORMATO DO ARQUIVO TXT (LINHA = UM PROCESSO)
---------------------------------------------------
- Cada linha representa 1 processo.
- Campos separados por `;`.
- Formato aceito por linha (exemplo):
  name; burst=INT; priority=INT; threads=INT; arrival=INT; cwd=/path; files=file1,file2,/abs/path/file3
- Campos:
  - `name` (opcional): identificador legível do processo.
  - `burst` (opcional): tempo (em ticks) de execução por thread (se faltar, gerado aleatoriamente).
  - `priority` (opcional): prioridade inteira (quanto menor, mais alta a prioridade).
  - `threads` (opcional): número de threads do processo.
  - `arrival` (opcional): tick de chegada do processo (0 = já disponível no início).
  - `cwd` (opcional): diretório de trabalho usado para resolver arquivos relativos.
  - `files` (opcional): lista separada por `,`. Caminhos relativos são resolvidos contra `cwd`. Arquivos não existentes são criados automaticamente pelo SO quando necessário.
- O simulador preenche aleatoriamente campos faltantes com valores plausíveis.
- Exemplo resumido:
  `Editor; burst=120; priority=2; threads=3; arrival=0; cwd=/home/user/docs; files=readme.md,config.json,/etc/hosts`

INTERFACE (visão geral)
-----------------------
- **Panel Controles (topo)**: seleção do escalonador, quantum, botões START/STOP/STEP/RESET, Carregar arquivo.
- **Painel Esquerda**:
  - Fila de prontos (ListBox).
  - Label de métricas rápidas (ticks, utilização CPU, processos, finalizados).
  - Status da CPU (ex.: "Ociosa" / "Executando P#:T#").
- **TabControl (centro)**:
  - *Processos*: DataGridView com PID, estado, prioridade, PC, restante; PropertyGrid à direita mostra o processo atualmente em execução (ou seleção atual).
  - *Threads*: DataGridView listando TID, PID pai, estado, restante.
  - *Memoria*: DataGridView com tabela de frames + **panelVisualizacaoMemoria** (visualização gráfica dinâmica da memória).
  - *Dispositivos I/O*: mostra filas de I/O ativas, quem está usando, operação e ticks restantes.
  - *Sistema de Arquivos*: TreeView navegável do FS virtual; clique com o botão direito para criar/remover arquivos e pastas.
  - *Metricas*: DataGrid com métricas cumulativas (threads, frames livres, etc).
- **Panel Logs (bottom)**: histórico textual de eventos (page faults, I/O, criação de arquivos, swap, terminação de threads).

COMO O SISTEMA É SIMULADO (resumo dos componentes)
-------------------------------------------------
1. **CPU / Escalonamento**
   - A simulação funciona por *ticks* (timer do Windows).
   - Escalonadores implementados:
     - **Round Robin**: alterna threads com quantum configurável; após quantum usado, thread volta para fila de prontos.
     - **FCFS**: primeiro a chegar (fila FIFO) recebe CPU até bloqueio/termino (não preemptivo, exceto por espera de I/O).
     - **Prioridade**: menor valor = prioridade mais alta; se surgir thread pronta com prioridade superior, preempção ocorre conforme implementação (troca para a ready).
   - `buttonSTEP` avança um tick sem rodar em tempo real; `START` inicia timer para ticks contínuos.

2. **Threads e Processos**
   - Processos contêm threads simuladas (não threads reais do SO).
   - Cada thread tem:
     - TID, PID pai, burst total (remaining), PC (program counter simulado), estado (New, Ready, Running, Waiting, Terminated), prioridade, páginas mapeadas, arquivos “abertos”.
   - Chegadas por `arrival` (tick) permitem processos que aparecem no futuro.

3. **Memória física, paginação e swap**
   - Memória dividida em *frames* (padrão: 64 frames — configurável no código).
   - Cada thread precisa de um número de páginas (página simulada corresponde a ~8 ticks de execução no modelo).
   - Alocação de páginas tenta usar frames livres; se insuficiente:
     - política aproximada **LRU**: escolhe frames menos recentemente usados como "victim" para swap-out (simulado).
     - Swap e operações de alocação são simuladas como I/O (com custo em ticks).
   - **Page fault**: quando a thread acessa uma página não mapeada (por exemplo, ao avançar PC para uma nova página), a thread fica em `Waiting` e é colocado numa fila de I/O para carregar a página (após ticks de I/O, o frame é alocado e a thread volta para Ready).
   - Liberação de frames quando thread termina.

4. **Visualização de Memória (panelVisualizacaoMemoria)**
   - Painel desenha dinamicamente uma grade de blocos (um bloco por frame).
   - Grade ajusta número de colunas/linhas com base no tamanho atual do painel (portanto a visualização é responsiva).
   - Cores representam dono do frame (P#:T#); blocos livres ficam cinza.
   - A intensidade (brilho) representa recência de uso (LRU visual): blocos usados recentemente aparecem mais brilhantes; os menos usados, mais escuros — ajudando a visualizar substituições.

5. **Sistema de Arquivos Virtual**
   - Estrutura de diretórios e arquivos em memória (VDirectory / VFile).
   - A árvore (TreeView) exibe diretórios e arquivos; clique direito abre menu de contexto para criar/remover arquivos e pastas.
   - `files` no arquivo de entrada liga um processo a arquivos existentes/criados; threads podem fazer operações de I/O simuladas em arquivos (que geram eventos de I/O).
   - Operações de I/O em arquivos atualizam `Modified` e `Size` (simulado).

6. **Dispositivos I/O e Filas**
   - Todas as operações de I/O (File I/O, Page Fault, Swap, Alocação) entram em `ioQueue` com um contador de ticks restante.
   - Aba *Dispositivos I/O* lista todas as operações em andamento: dispositivo (Disco/FS, Memória/Swap), owner (P:T), operação, destino (arquivo ou página), ticks restantes e estado da thread.
   - Threads que aguardam I/O ficam em `Waiting` até a operação completar; depois retornam à fila de prontos.
   - Executando (runningThread) é mostrado como “CPU” nesta visão.

7. **PropertyGridProcessos**
   - `propertyGridProcessos` é atualizado automaticamente para mostrar o **processo do thread atualmente em execução**, com todos os dados do processo (PID, prioridade, threads, chegada, etc).
   - Se não houver thread em execução, mantém a seleção atual do usuário (ou mostra o primeiro processo se nada estiver selecionado).

8. **Logs e Métricas**
   - Logs textuais documentam eventos importantes: carregamento de arquivo, criação/remoção de arquivos, page faults, swap out/in, término de threads, operações I/O, START/STOP/RESET.
   - Métricas exibidas:
     - Ticks totais.
     - Ticks CPU em uso.
     - Utilização CPU (%).
     - Quantidade de processos, finalizados, tamanho da fila de prontos.
     - Na aba *Metricas*: total de processos, total de threads, threads finalizadas, threads esperando I/O, frames livres etc.

COMO INTERAGIR COM O SISTEMA (fluxo típico)
------------------------------------------
1. Preparar um arquivo `.txt` com processos (ou usar os samples gerados).
2. Carregar o arquivo: **Carregar Processos**.
3. Escolher escalonador e ajustar quantum.
4. **START**: observe:
   - panelVisualizacaoMemoria preenchendo frames para threads alocadas;
   - logs de page faults e I/O;
   - Aba Dispositivos I/O mostrando operações em andamento.
5. Se quiser analisar passo a passo, usar **STEP**.
6. Criar/Remover arquivos pelo TreeView (clicar com botão direito).
7. Pausar com **STOP** e retomar com **START**.
8. Reset com **RESET** para limpar tudo e começar de novo.

COMPORTAMENTOS IMPORTANTES A LEMBRAR
-----------------------------------
- Tudo é **simulado**: não são processos ou threads reais do Windows — o objetivo é modelar e visualizar conceitos de Sistemas Operacionais.
- Operações de swap/paging e I/O têm custo em ticks; isso pode atrasar threads e afetar escalonamento.
- Valores aleatórios (quando campos faltam) servem para tornar o simulador flexível e gerar cenários diversos automaticamente.
- A visualização de memória é sensível ao tamanho do painel — redimensione a janela para ver mais/menos blocos por linha.

PERSONALIZAÇÕES POSSÍVEIS (onde editar no código)
-------------------------------------------------
- `totalFrames` altera a quantidade de frames simulados.
- `ioDeviceTicksPerOperation` altera custo padrão de I/O.
- Timer interval (velocidade dos ticks) configurável via `timerSO.Interval`.
- Adição de novos escalonadores, políticas de substituição (ex.: FIFO, CLOCK) e métricas (turnaround/response time) pode ser feita estendendo as rotinas de schedule / coleta de métricas.
- Exportar snapshots do estado (memória + FS + processos) ou gerar gráficos (Gantt) são extensões naturais fáceis de implementar.

ERROS COMUNS / TROUBLESHOOTING
------------------------------
- Se não abrir o executável: verifique se o runtime .NET adequado está instalado.
- Se o painel de memória aparecer vazio, tente **Redimensionar** a janela (força redraw).
- Erros ao carregar arquivo: verifique o formato das linhas; linhas vazias são ignoradas. O parser tenta ignorar erros e gerar defaults, mas linhas extremamente malformadas podem ser puladas.
- Se as operações de I/O não aparecerem na aba *Dispositivos I/O*, certifique-se de que o simulador está rodando (START) e que há threads produzindo I/O (alguns processos simples podem não acionar I/O).

EXEMPLOS E TESTES SUGERIDOS
--------------------------
- Teste com processos que têm `arrival` > 0 para verificar processos que aparecem no futuro.
- Teste com `burst` muito grande e poucos frames para forçar swaps frequentes.
- Coloque muitos processos com `files` apontando para mesmos arquivos para observar concorrência de I/O (simulada).
- Alterne entre escalonadores e compare utilização/throughput/espera.

CRÉDITOS
--------
Desenvolvido por:
- Pedro Paulo Santos de Jesus Junior - 113538
- Gabriel Marques Paulon - 113142

Estudantes de Engenharia da Computação — Fundação Hermínio Ometto (FHO)
Disciplina: Sistemas Operacionais
Professor: Mauricio Acconcia Dias

Obrigado e bom uso!

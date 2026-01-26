using SwarNet.Enums;

namespace SwarNet
{
    public partial class MainWindow : Form
    {
        private GameServer _server;
        private GameClient _client;
        private DiscoveryBroadcaster _broadcaster;
        private DiscoveryListener _discoveryListener;
        

        public MainWindow()
        {
            InitializeComponent();
            btnSendChat.Enabled = false;

            SetupGameBoards();
        }

        private void SetupGameBoards()
        {
           
        }

        private void btnHost_Click(object sender, EventArgs e)
        {
            if (_server != null)
            {
                AppendLog("Server already running.");
                return;
            }

            _server = new GameServer();
            _server.OnLogMessage += AppendLog;
            _server.OnMessageReceived += msg =>
            {
                this.Invoke(() => AppendLog($"Opponent: {msg.Type} → {msg.Payload}"));
            };

            // New: Stop broadcast when client connects
            _server.OnClientConnectedEvent += () =>
            {
                this.Invoke(() =>
                {
                    _broadcaster?.Stop();
                    AppendLog("Client connected – UDP broadcast stopped.");
                    btnConnect.Enabled = true;
                    btnSendChat.Enabled = true;
                });
            };

            // New: Handle client disconnect
            _server.OnClientDisconnected += () =>
            {
                this.Invoke(() =>
                {
                    AppendLog("Opponent disconnected from the game.");
                    btnSendChat.Enabled = false;
                });
            };

            _broadcaster = new DiscoveryBroadcaster();
            _broadcaster.StartBroadcast(55555, Environment.MachineName);

            _server.Start(55555);

            AppendLog("Hosting SwarNet game... Broadcasting every 5 seconds");
            btnHost.Enabled = false;
        }

        private void btnFindGames_Click(object sender, EventArgs e)
        {
            if (_discoveryListener != null)
            {
                AppendLog("Already searching...");
                return;
            }

            _discoveryListener = new DiscoveryListener();

            _discoveryListener.OnHostDiscovered += (ip, port, hostName) =>
            {
                this.Invoke(new Action(() =>
                {
                    string display = $"{hostName} ({ip}:{port})";
                    if (!listBoxHosts.Items.Contains(display))
                    {
                        listBoxHosts.Items.Add(display);
                        AppendLog($"Discovered: {display}");
                    }
                }));
            };

            _discoveryListener.StartListening();
            AppendLog("Searching for SwarNet games on local network...");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (listBoxHosts.SelectedItem == null)
            {
                AppendLog("Select a host from the list first.");
                return;
            }

            string selected = listBoxHosts.SelectedItem.ToString();
            var ipPart = selected.Substring(selected.IndexOf('(') + 1).TrimEnd(')');
            var parts = ipPart.Split(':');
            string ip = parts[0];
            int port = int.Parse(parts[1]);

            if (_client != null)
            {
                AppendLog("Already connected.");
                return;
            }

            _client = new GameClient();
            _client.OnLogMessage += AppendLog;
            _client.OnMessageReceived += msg =>
            {
                this.Invoke(() => AppendLog($"Host: {msg.Type} → {msg.Payload}"));

                if (msg.Type == MessageType.ConnectAccepted)
                {
                    btnSendChat.Enabled = true;  // ← Enable only after receiving ConnectAccepted
                    AppendLog("Connection confirmed – chat enabled!");
                }
            };

            // New: Stop discovery listener after connect
            _client.OnDisconnected += () =>
            {
                this.Invoke(() =>
                {
                    AppendLog("Lost connection to host. Game session ended.");
                    btnSendChat.Enabled = false;
                    listBoxHosts.Items.Clear();
                });
            };

            _client.Connect(ip, port);

            _discoveryListener?.Stop();
            AppendLog("Connected – stopped searching for hosts.");

            AppendLog($"Connecting to {selected}...");
            btnConnect.Enabled = false;
            btnSendChat.Enabled = true;
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChatInput.Text))
                return;

            string chatText = txtChatInput.Text.Trim();
            var msg = NetworkMessage.Chat(chatText);

            if (_client != null)  // Client mode
            {
                _client.SendMessage(msg);
                AppendLog($"You (Client): ChatMessage → {chatText}");
            }
            else if (_server != null && _server.ClientConnected)  // Host mode – only if someone connected
            {
                _server.SendMessage(msg);
                AppendLog($"You (Host): ChatMessage → {chatText}");
            }
            else
            {
                AppendLog("No active connection – cannot send chat.");
                return;
            }

            txtChatInput.Clear();
        }

        private void AppendLog(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => AppendLog(message)));
                return;
            }

            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            rtbLog.ScrollToCaret();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _client?.Disconnect();
            _server?.Stop();
            _broadcaster?.Stop();
            _discoveryListener?.Stop();

            AppendLog("SwarNet shutting down...");
        }

        private bool IsInShipPlacement = false;
        
    }
}
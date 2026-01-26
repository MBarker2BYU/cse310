using SwarNet.Enums;
using SwarNet.Networking;

namespace SwarNet.Forms
{
    public partial class MainWindow : Form
    {
        private GameServer m_Server;
        private GameClient m_Client;
        private DiscoveryBroadcaster m_Broadcaster;
        private DiscoveryListener m_DiscoveryListener;
        

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
            if (m_Server != null)
            {
                AppendLog("Server already running.");
                return;
            }

            m_Server = new GameServer();
            m_Server.OnLogMessage += AppendLog;
            m_Server.OnMessageReceived += msg =>
            {
                this.Invoke(() => AppendLog($"Opponent: {msg.Type} → {msg.Payload}"));
            };

            // New: Stop broadcast when client connects
            m_Server.OnClientConnectedEvent += () =>
            {
                this.Invoke(() =>
                {
                    m_Broadcaster?.Stop();
                    AppendLog("Client connected – UDP broadcast stopped.");
                    btnConnect.Enabled = true;
                    btnSendChat.Enabled = true;
                });
            };

            // New: Handle client disconnect
            m_Server.OnClientDisconnected += () =>
            {
                this.Invoke(() =>
                {
                    AppendLog("Opponent disconnected from the game.");
                    btnSendChat.Enabled = false;
                });
            };

            m_Broadcaster = new DiscoveryBroadcaster();
            m_Broadcaster.StartBroadcast(55555, Environment.MachineName);

            m_Server.Start(55555);

            AppendLog("Hosting SwarNet game... Broadcasting every 5 seconds");
            btnHost.Enabled = false;
        }

        private void btnFindGames_Click(object sender, EventArgs e)
        {
            if (m_DiscoveryListener != null)
            {
                AppendLog("Already searching...");
                return;
            }

            m_DiscoveryListener = new DiscoveryListener();

            m_DiscoveryListener.OnHostDiscovered += (ip, port, hostName) =>
            {
                this.Invoke(new Action(() =>
                {
                    var display = $"{hostName} ({ip}:{port})";
                    if (!listBoxHosts.Items.Contains(display))
                    {
                        listBoxHosts.Items.Add(display);
                        AppendLog($"Discovered: {display}");
                    }
                }));
            };

            m_DiscoveryListener.StartListening();
            AppendLog("Searching for SwarNet games on local network...");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (listBoxHosts.SelectedItem == null)
            {
                AppendLog("Select a host from the list first.");
                return;
            }

            var selected = listBoxHosts.SelectedItem.ToString();
            var ipPart = selected.Substring(selected.IndexOf('(') + 1).TrimEnd(')');
            var parts = ipPart.Split(':');
            var ip = parts[0];
            var port = int.Parse(parts[1]);

            if (m_Client != null)
            {
                AppendLog("Already connected.");
                return;
            }

            m_Client = new GameClient();
            m_Client.OnLogMessage += AppendLog;
            m_Client.OnMessageReceived += msg =>
            {
                this.Invoke(() => AppendLog($"Host: {msg.Type} → {msg.Payload}"));

                if (msg.Type == MessageType.ConnectAccepted)
                {
                    btnSendChat.Enabled = true;  // ← Enable only after receiving ConnectAccepted
                    AppendLog("Connection confirmed – chat enabled!");
                }
            };

            // New: Stop discovery listener after connect
            m_Client.OnDisconnected += () =>
            {
                this.Invoke(() =>
                {
                    AppendLog("Lost connection to host. Game session ended.");
                    btnSendChat.Enabled = false;
                    listBoxHosts.Items.Clear();
                });
            };

            m_Client.Connect(ip, port);

            m_DiscoveryListener?.Stop();
            AppendLog("Connected – stopped searching for hosts.");

            AppendLog($"Connecting to {selected}...");
            btnConnect.Enabled = false;
            btnSendChat.Enabled = true;
        }

        private void btnSendChat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChatInput.Text))
                return;

            var chatText = txtChatInput.Text.Trim();
            var msg = NetworkMessage.Chat(chatText);

            if (m_Client != null)  // Client mode
            {
                m_Client.SendMessage(msg);
                AppendLog($"You (Client): ChatMessage → {chatText}");
            }
            else if (m_Server != null && m_Server.ClientConnected)  // Host mode – only if someone connected
            {
                m_Server.SendMessage(msg);
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
            m_Client?.Disconnect();
            m_Server?.Stop();
            m_Broadcaster?.Stop();
            m_DiscoveryListener?.Stop();

            AppendLog("SwarNet shutting down...");
        }

        private bool m_IsInShipPlacement = false;
        
    }
}
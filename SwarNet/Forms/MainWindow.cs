// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="MainWindow.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;
using SwarNet.GameLogic;
using SwarNet.Networking;

namespace SwarNet.Forms
{
    /// <summary>
    /// Class MainWindow.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MainWindow : Form
    {
        /// <summary>
        /// The m server
        /// </summary>
        private GameServer m_Server;
        /// <summary>
        /// The m client
        /// </summary>
        private GameClient m_Client;
        /// <summary>
        /// The m broadcaster
        /// </summary>
        private DiscoveryBroadcaster m_Broadcaster;
        /// <summary>
        /// The m discovery listener
        /// </summary>
        private DiscoveryListener m_DiscoveryListener;

        /// <summary>
        /// The m game host
        /// </summary>
        private GameHost m_GameHost;
        /// <summary>
        /// The m game session
        /// </summary>
        private GameSession m_GameSession;


        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            btnSendChat.Enabled = false;
        }

        /// <summary>
        /// Handles the Click event of the btnHost control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnHost_Click(object sender, System.EventArgs e)
        {
            if (m_Server != null)
            {
                AppendLog("Server already running.");
                return;
            }

            m_Server = new GameServer();

            m_Server.LogMessage += AppendLog;
            m_Server.MessageReceived += msg =>
            {
                this.Invoke(() => AppendLog($"Opponent: {msg.Type} → {msg.Payload}"));

                if (msg.Type == MessageType.ChatMessage)
                {
                    AppendMessage("Player2", msg.Payload);
                }
            };

            // New: Stop broadcast when client connects
            m_Server.ClientConnectedEvent += () =>
            {
                this.Invoke(() =>
                {
                    m_Broadcaster?.Stop();
                    AppendLog("Client connected – UDP broadcast stopped.");
                    btnConnect.Enabled = true;
                    btnSendChat.Enabled = true;

                    m_GameHost = new GameHost(m_Server, fleetAttackBoard, fleetStatusBoard, lblTurn);
                });
            };

            // New: Handle client disconnect
            m_Server.ClientDisconnected += () =>
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

        /// <summary>
        /// Handles the Click event of the btnFindGames control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnFindGames_Click(object sender, System.EventArgs e)
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

        /// <summary>
        /// Handles the Click event of the btnConnect control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnConnect_Click(object sender, System.EventArgs e)
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

            m_GameSession = new GameSession(m_Client, fleetAttackBoard, fleetStatusBoard, lblTurn);

            m_Client.LogMessage += AppendLog;
            m_Client.MessageReceived += msg =>
            {
                this.Invoke(() => AppendLog($"Host: {msg.Type} → {msg.Payload}"));

                if (msg.Type == MessageType.ConnectAccepted)
                {
                    btnSendChat.Enabled = true;  // ← Enable only after receiving ConnectAccepted
                    AppendLog("Connection confirmed – chat enabled!");
                }

                if (msg.Type == MessageType.ChatMessage)
                {
                    AppendMessage("Player1", msg.Payload);
                }
            };

            // New: Stop discovery listener after connect
            m_Client.Disconnected += () =>
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

        /// <summary>
        /// Handles the Click event of the btnSendChat control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSendChat_Click(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtChatInput.Text))
                return;

            var chatText = txtChatInput.Text.Trim();
            var msg = NetworkMessage.Chat(chatText);


            if (m_Client != null)  // Client mode
            {
                m_Client.SendMessage(msg);
                AppendLog($"You (Client): ChatMessage → {chatText}");

                AppendMessage("Player2", chatText, true);
            }
            else if (m_Server != null && m_Server.ClientConnected)  // Host mode – only if someone connected
            {
                m_Server.SendMessage(msg);
                AppendLog($"You (Host): ChatMessage → {chatText}");

                AppendMessage("Player1", chatText, true);
            }
            else
            {
                AppendLog("No active connection – cannot send chat.");
                return;
            }

            txtChatInput.Clear();
        }

        /// <summary>
        /// Appends the log.
        /// </summary>
        /// <param name="message">The message.</param>
        private void AppendLog(string message)
        {
            if(rtbLog.IsDisposed)
                return;

            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action(() => AppendLog(message)));
                return;
            }

            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\r\n");
            rtbLog.ScrollToCaret();
        }

        /// <summary>
        /// Appends the message.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="text">The text.</param>
        /// <param name="isMine">if set to <c>true</c> [is mine].</param>
        private void AppendMessage(string sender, string text, bool isMine = false)
        {
            if (richTextBoxChat.InvokeRequired)
            {
                richTextBoxChat.Invoke(new Action(() => AppendMessage(sender, text, isMine)));
                return;
            }

            richTextBoxChat.SelectionStart = richTextBoxChat.TextLength;
            richTextBoxChat.SelectionLength = 0;

            // Timestamp
            richTextBoxChat.SelectionColor = Color.Gray;
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Italic);
            richTextBoxChat.AppendText($"[{DateTime.Now:HH:mm:ss}] ");

            // Sender name
            richTextBoxChat.SelectionColor = isMine ? Color.DarkBlue : Color.DarkGreen;
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Bold);
            richTextBoxChat.AppendText(sender + ": ");

            // Message text
            richTextBoxChat.SelectionColor = richTextBoxChat.ForeColor;
            richTextBoxChat.SelectionFont = new Font(richTextBoxChat.Font, FontStyle.Regular);

            // Indent if not mine (or reverse for Skype-style left/right alignment)
            if (!isMine)
            {
                richTextBoxChat.SelectionIndent = 40;   // pixels indent
                richTextBoxChat.SelectionHangingIndent = -20;  // optional: hanging for multi-line
            }
            else
            {
                richTextBoxChat.SelectionIndent = 0;
            }

            richTextBoxChat.AppendText(text + Environment.NewLine);

            // Reset indent & color
            richTextBoxChat.SelectionIndent = 0;
            richTextBoxChat.SelectionHangingIndent = 0;

            // Scroll to bottom
            richTextBoxChat.SelectionStart = richTextBoxChat.TextLength;
            richTextBoxChat.ScrollToCaret();
        }

        /// <summary>
        /// Handles the FormClosing event of the FormMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Client?.Disconnect();
            m_Server?.Stop();
            m_Broadcaster?.Stop();
            m_DiscoveryListener?.Stop();

            AppendLog("SwarNet shutting down...");
        }

        /// <summary>
        /// The m is in ship placement
        /// </summary>
        private bool m_IsInShipPlacement = false;

    }
}
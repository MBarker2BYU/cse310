// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="GameServer.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net;
using System.Net.Sockets;
using System.Text;
using SwarNet.Enums;

namespace SwarNet.Networking
{
    /// <summary>
    /// Class GameServer.
    /// </summary>
    public class GameServer
    {
        /// <summary>
        /// The m listener
        /// </summary>
        private TcpListener m_Listener;
        /// <summary>
        /// The m client
        /// </summary>
        private TcpClient m_Client;
        /// <summary>
        /// The m stream
        /// </summary>
        private NetworkStream m_Stream;
        /// <summary>
        /// The m running
        /// </summary>
        private bool m_Running;

        /// <summary>
        /// Occurs when [log message].
        /// </summary>
        public event Action<string>? LogMessage;
        /// <summary>
        /// Occurs when [message received].
        /// </summary>
        public event Action<NetworkMessage>? MessageReceived;
        /// <summary>
        /// Occurs when [client connected event].
        /// </summary>
        public event Action? ClientConnectedEvent;     // New: when client connects
        /// <summary>
        /// Occurs when [client disconnected].
        /// </summary>
        public event Action? ClientDisconnected;       // New: when client disconnects

        /// <summary>
        /// Starts the specified port.
        /// </summary>
        /// <param name="port">The port.</param>
        public void Start(int port = 55555)
        {
            try
            {
                m_Listener = new TcpListener(IPAddress.Any, port);
                m_Listener.Start();
                m_Running = true;

                LogMessage?.Invoke($"SwarNet server started on port {port}. Waiting for opponent...");
                LogMessage?.Invoke("UDP discovery broadcast is active (every 5 seconds)");

                var acceptThread = new Thread(AcceptClientLoop);
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"Failed to start server: {ex.Message}");
            }
        }

        /// <summary>
        /// Accepts the client loop.
        /// </summary>
        private void AcceptClientLoop()
        {
            while (m_Running)
            {
                try
                {
                    m_Client = m_Listener.AcceptTcpClient();
                    m_Stream = m_Client.GetStream();

                    ClientConnected = true;

                    LogMessage?.Invoke("Opponent connected! Starting game session...");
                    ClientConnectedEvent?.Invoke();  // Trigger broadcast stop

                    SendMessage(new NetworkMessage
                    {
                        Type = MessageType.ConnectAccepted,
                        Payload = "Welcome to SwarNet – Sea Warfare Network by ShadowWorx Systems!"
                    });

                    var receiveThread = new Thread(ReceiveLoop);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();

                    break; // Only one client for this module
                }
                catch (Exception ex)
                {
                    if (m_Running)
                        LogMessage?.Invoke($"Accept error: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Receives the loop.
        /// </summary>
        private void ReceiveLoop()
        {
            var buffer = new byte[1024];
            var messageBuilder = new StringBuilder();

            while (m_Running && m_Client?.Connected == true)
            {
                try
                {
                    var bytesRead = m_Stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        LogMessage?.Invoke("Connection lost: Opponent closed the game.");
                        break;
                    }

                    var received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuilder.Append(received);

                    var data = messageBuilder.ToString();
                    int newlinePos;

                    while ((newlinePos = data.IndexOf('\n')) >= 0)
                    {
                        var completeMessage = data.Substring(0, newlinePos).Trim();
                        if (!string.IsNullOrEmpty(completeMessage))
                        {
                            try
                            {
                                var msg = NetworkMessage.FromString(completeMessage);
                                MessageReceived?.Invoke(msg);
                                LogMessage?.Invoke($"[IN] {msg.Type}: {msg.Payload}");
                            }
                            catch (Exception ex)
                            {
                                LogMessage?.Invoke($"Message parse error: {ex.Message} → {completeMessage}");
                            }
                        }

                        data = data.Substring(newlinePos + 1);
                        messageBuilder.Clear();
                        messageBuilder.Append(data);
                    }
                }
                catch (Exception ex)
                {
                    if (m_Running)
                        LogMessage?.Invoke($"Receive error: {ex.Message}. Opponent may have closed.");
                    break;
                }
            }

            LogMessage?.Invoke("Opponent disconnected.");
            ClientDisconnected?.Invoke();  // Notify UI
            Cleanup();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(NetworkMessage message)
        {
            if (m_Stream == null || !m_Client?.Connected == true)
                return;

            try
            {
                var text = message.ToString() + "\n";
                var data = Encoding.UTF8.GetBytes(text);
                m_Stream.Write(data, 0, data.Length);
                m_Stream.Flush();

                LogMessage?.Invoke($"[OUT] {message.Type}: {message.Payload}");
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"Send error: {ex.Message}");
            }
        }

        /// <summary>
        /// Gets a value indicating whether [client connected].
        /// </summary>
        /// <value><c>true</c> if [client connected]; otherwise, <c>false</c>.</value>
        public bool ClientConnected { get; private set; } = false;

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        private void Cleanup()
        {
            m_Stream?.Close();
            m_Client?.Close();
            m_Listener?.Stop();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            m_Running = false;
            Cleanup();
            LogMessage?.Invoke("SwarNet server stopped.");
        }
    }
}
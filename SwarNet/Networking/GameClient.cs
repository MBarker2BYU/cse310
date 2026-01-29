// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="GameClient.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net.Sockets;
using System.Text;
using SwarNet.Enums;

namespace SwarNet.Networking
{
    /// <summary>
    /// Class GameClient.
    /// </summary>
    public class GameClient
    {
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
        /// Occurs when [disconnected].
        /// </summary>
        public event Action? Disconnected;  // New: notify UI when connection lost

        /// <summary>
        /// Connects the specified host ip.
        /// </summary>
        /// <param name="hostIp">The host ip.</param>
        /// <param name="port">The port.</param>
        public void Connect(string hostIp, int port = 55555)
        {
            try
            {
                m_Client = new TcpClient();
                m_Client.Connect(hostIp, port);
                m_Stream = m_Client.GetStream();
                m_Running = true;

                LogMessage?.Invoke($"Connected to SwarNet host at {hostIp}:{port}");

                SendMessage(new NetworkMessage
                {
                    Type = MessageType.ConnectRequest,
                    Payload = "Client ready to join"
                });

                var receiveThread = new Thread(ReceiveLoop);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                LogMessage?.Invoke($"Connection failed: {ex.Message}");
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
                        LogMessage?.Invoke("Connection lost: Host closed the game.");
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
                        LogMessage?.Invoke($"Connection error: {ex.Message}. Host may have closed.");
                    break;
                }
            }

            LogMessage?.Invoke("Disconnected from host.");
            Disconnected?.Invoke();  // Notify UI
            Cleanup();
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void SendMessage(NetworkMessage message)
        {
            if (m_Stream == null || !m_Client?.Connected == true)
            {
                LogMessage?.Invoke("Cannot send - not connected");
                return;
            }

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
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            m_Running = false;
            Cleanup();
            LogMessage?.Invoke("Client disconnected gracefully.");
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        private void Cleanup()
        {
            m_Stream?.Close();
            m_Client?.Close();
            m_Stream = null;
            m_Client = null;
        }
    }
}
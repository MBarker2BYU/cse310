using System.Net.Sockets;
using System.Text;
using SwarNet.Enums;

namespace SwarNet.Networking
{
    public class GameClient
    {
        private TcpClient m_Client;
        private NetworkStream m_Stream;
        private bool m_Running;

        public event Action<string>? LogMessage;
        public event Action<NetworkMessage>? MessageReceived;
        public event Action? Disconnected;  // New: notify UI when connection lost

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

        public void Disconnect()
        {
            m_Running = false;
            Cleanup();
            LogMessage?.Invoke("Client disconnected gracefully.");
        }

        private void Cleanup()
        {
            m_Stream?.Close();
            m_Client?.Close();
            m_Stream = null;
            m_Client = null;
        }
    }
}
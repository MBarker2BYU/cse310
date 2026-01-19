using SwarNet.Enums;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SwarNet
{
    public class GameClient
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private bool _running;

        public event Action<string> OnLogMessage;
        public event Action<NetworkMessage> OnMessageReceived;
        public event Action OnDisconnected;  // New: notify UI when connection lost

        public void Connect(string hostIp, int port = 55555)
        {
            try
            {
                _client = new TcpClient();
                _client.Connect(hostIp, port);
                _stream = _client.GetStream();
                _running = true;

                OnLogMessage?.Invoke($"Connected to SwarNet host at {hostIp}:{port}");

                SendMessage(new NetworkMessage
                {
                    Type = MessageType.ConnectRequest,
                    Payload = "Client ready to join"
                });

                Thread receiveThread = new Thread(ReceiveLoop);
                receiveThread.IsBackground = true;
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                OnLogMessage?.Invoke($"Connection failed: {ex.Message}");
            }
        }

        private void ReceiveLoop()
        {
            byte[] buffer = new byte[1024];
            StringBuilder messageBuilder = new StringBuilder();

            while (_running && _client?.Connected == true)
            {
                try
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        OnLogMessage?.Invoke("Connection lost: Host closed the game.");
                        break;
                    }

                    string received = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    messageBuilder.Append(received);

                    string data = messageBuilder.ToString();
                    int newlinePos;

                    while ((newlinePos = data.IndexOf('\n')) >= 0)
                    {
                        string completeMessage = data.Substring(0, newlinePos).Trim();
                        if (!string.IsNullOrEmpty(completeMessage))
                        {
                            try
                            {
                                var msg = NetworkMessage.FromString(completeMessage);
                                OnMessageReceived?.Invoke(msg);
                                OnLogMessage?.Invoke($"[IN] {msg.Type}: {msg.Payload}");
                            }
                            catch (Exception ex)
                            {
                                OnLogMessage?.Invoke($"Message parse error: {ex.Message} → {completeMessage}");
                            }
                        }

                        data = data.Substring(newlinePos + 1);
                        messageBuilder.Clear();
                        messageBuilder.Append(data);
                    }
                }
                catch (Exception ex)
                {
                    if (_running)
                        OnLogMessage?.Invoke($"Connection error: {ex.Message}. Host may have closed.");
                    break;
                }
            }

            OnLogMessage?.Invoke("Disconnected from host.");
            OnDisconnected?.Invoke();  // Notify UI
            Cleanup();
        }

        public void SendMessage(NetworkMessage message)
        {
            if (_stream == null || !_client?.Connected == true)
            {
                OnLogMessage?.Invoke("Cannot send - not connected");
                return;
            }

            try
            {
                string text = message.ToString() + "\n";
                byte[] data = Encoding.UTF8.GetBytes(text);
                _stream.Write(data, 0, data.Length);
                _stream.Flush();

                OnLogMessage?.Invoke($"[OUT] {message.Type}: {message.Payload}");
            }
            catch (Exception ex)
            {
                OnLogMessage?.Invoke($"Send error: {ex.Message}");
            }
        }

        public void Disconnect()
        {
            _running = false;
            Cleanup();
            OnLogMessage?.Invoke("Client disconnected gracefully.");
        }

        private void Cleanup()
        {
            _stream?.Close();
            _client?.Close();
            _stream = null;
            _client = null;
        }
    }
}
using SwarNet.Enums;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SwarNet
{
    public class GameServer
    {
        private TcpListener _listener;
        private TcpClient _client;
        private NetworkStream _stream;
        private bool _running;

        public event Action<string> OnLogMessage;
        public event Action<NetworkMessage> OnMessageReceived;
        public event Action OnClientConnectedEvent;     // New: when client connects
        public event Action OnClientDisconnected;       // New: when client disconnects

        public void Start(int port = 55555)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Any, port);
                _listener.Start();
                _running = true;

                OnLogMessage?.Invoke($"SwarNet server started on port {port}. Waiting for opponent...");
                OnLogMessage?.Invoke("UDP discovery broadcast is active (every 5 seconds)");

                Thread acceptThread = new Thread(AcceptClientLoop);
                acceptThread.IsBackground = true;
                acceptThread.Start();
            }
            catch (Exception ex)
            {
                OnLogMessage?.Invoke($"Failed to start server: {ex.Message}");
            }
        }

        private void AcceptClientLoop()
        {
            while (_running)
            {
                try
                {
                    _client = _listener.AcceptTcpClient();
                    _stream = _client.GetStream();

                    ClientConnected = true;

                    OnLogMessage?.Invoke("Opponent connected! Starting game session...");
                    OnClientConnectedEvent?.Invoke();  // Trigger broadcast stop

                    SendMessage(new NetworkMessage
                    {
                        Type = MessageType.ConnectAccepted,
                        Payload = "Welcome to SwarNet – Sea Warfare Network by ShadowWorx Systems!"
                    });

                    Thread receiveThread = new Thread(ReceiveLoop);
                    receiveThread.IsBackground = true;
                    receiveThread.Start();

                    break; // Only one client for this module
                }
                catch (Exception ex)
                {
                    if (_running)
                        OnLogMessage?.Invoke($"Accept error: {ex.Message}");
                }
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
                        OnLogMessage?.Invoke("Connection lost: Opponent closed the game.");
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
                        OnLogMessage?.Invoke($"Receive error: {ex.Message}. Opponent may have closed.");
                    break;
                }
            }

            OnLogMessage?.Invoke("Opponent disconnected.");
            OnClientDisconnected?.Invoke();  // Notify UI
            Cleanup();
        }

        public void SendMessage(NetworkMessage message)
        {
            if (_stream == null || !_client?.Connected == true)
                return;

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

        public bool ClientConnected { get; private set; } = false;

        private void Cleanup()
        {
            _stream?.Close();
            _client?.Close();
            _listener?.Stop();
        }

        public void Stop()
        {
            _running = false;
            Cleanup();
            OnLogMessage?.Invoke("SwarNet server stopped.");
        }
    }
}
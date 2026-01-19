// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="DiscoveryListener.cs" company="SwarNet">
//     Copyright (c) ShadowWorx Systems, LLC. All rights reserved.
// </copyright>
// <summary></summary>
// *********************************************************************** 

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SwarNet
{
    /// <summary>
    /// Class DiscoveryListener.
    /// </summary>
    public class DiscoveryListener
    {
        /// <summary>
        /// The UDP client
        /// </summary>
        private UdpClient _udpClient;
        /// <summary>
        /// The running
        /// </summary>
        private bool _running;

        /// <summary>
        /// Occurs when [on host discovered].
        /// </summary>
        public event Action<string, int, string> OnHostDiscovered;  // IP, TCP port, hostname

        /// <summary>
        /// Starts the listening.
        /// </summary>
        /// <param name="discoveryPort">The discovery port.</param>
        public void StartListening(int discoveryPort = 55556)
        {
            try
            {
                _udpClient = new UdpClient(discoveryPort);
                _udpClient.EnableBroadcast = true;
                _running = true;

                Thread listenThread = new Thread(() =>
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    while (_running)
                    {
                        try
                        {
                            byte[] data = _udpClient.Receive(ref remoteEndPoint);
                            string message = Encoding.UTF8.GetString(data);

                            if (message.StartsWith("BATTLESHIP_HOST|"))
                            {
                                var parts = message.Split('|');
                                if (parts.Length == 3)
                                {
                                    if (int.TryParse(parts[1], out int tcpPort))
                                    {
                                        string hostName = parts[2];
                                        string ip = remoteEndPoint.Address.ToString();

                                        OnHostDiscovered?.Invoke(ip, tcpPort, hostName);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Silent fail - keep listening
                        }
                    }
                })
                { IsBackground = true };

                listenThread.Start();
            }
            catch (Exception ex)
            {
                // Could add logging here later
            }
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            _running = false;
            _udpClient?.Close();
        }
    }
}
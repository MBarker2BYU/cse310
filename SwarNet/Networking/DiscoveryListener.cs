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

namespace SwarNet.Networking
{
    /// <summary>
    /// Class DiscoveryListener.
    /// </summary>
    public class DiscoveryListener
    {
        /// <summary>
        /// The UDP client
        /// </summary>
        private UdpClient m_UdpClient;
        /// <summary>
        /// The running
        /// </summary>
        private bool m_Running;

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
                m_UdpClient = new UdpClient(discoveryPort);
                m_UdpClient.EnableBroadcast = true;
                m_Running = true;

                var listenThread = new Thread(() =>
                {
                    var remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    while (m_Running)
                    {
                        try
                        {
                            var data = m_UdpClient.Receive(ref remoteEndPoint);
                            var message = Encoding.UTF8.GetString(data);

                            if (message.StartsWith("BATTLESHIP_HOST|"))
                            {
                                var parts = message.Split('|');
                                if (parts.Length == 3)
                                {
                                    if (int.TryParse(parts[1], out var tcpPort))
                                    {
                                        var hostName = parts[2];
                                        var ip = remoteEndPoint.Address.ToString();

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
            m_Running = false;
            m_UdpClient?.Close();
        }
    }
}
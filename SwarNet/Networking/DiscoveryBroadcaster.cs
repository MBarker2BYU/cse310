// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="DiscoveryBroadcaster.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SwarNet.Networking
{
    /// <summary>
    /// Class DiscoveryBroadcaster.
    /// </summary>
    public class DiscoveryBroadcaster
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
        /// The discovery port
        /// </summary>
        private readonly int m_DiscoveryPort = 55556;
        /// <summary>
        /// The identifier
        /// </summary>
        private readonly string m_Identifier = "BATTLESHIP_HOST";

        /// <summary>
        /// Starts broadcasting the server's presence every 5 seconds.
        /// </summary>
        /// <param name="tcpGamePort">The TCP port the game server is listening on</param>
        /// <param name="hostName">The hostname/computer name to include in the broadcast</param>
        public void StartBroadcast(int tcpGamePort, string hostName)
        {
            try
            {
                m_UdpClient = new UdpClient();
                m_UdpClient.EnableBroadcast = true;
                m_Running = true;

                var message = $"{m_Identifier}|{tcpGamePort}|{hostName}";

                var broadcastThread = new Thread(() =>
                {
                    var broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, m_DiscoveryPort);

                    while (m_Running)
                    {
                        try
                        {
                            var data = Encoding.UTF8.GetBytes(message);
                            m_UdpClient.Send(data, data.Length, broadcastEndpoint);
                        }
                        catch
                        {
                            // Silent fail - keep trying to broadcast
                        }

                        Thread.Sleep(5000); // Broadcast every 5 seconds
                    }
                })
                {
                    IsBackground = true
                };

                broadcastThread.Start();
            }
            catch (Exception ex)
            {
                // Could log this later if needed
            }
        }

        /// <summary>
        /// Stops broadcasting and cleans up resources.
        /// </summary>
        public void Stop()
        {
            m_Running = false;
            m_UdpClient?.Close();
        }
    }
}
// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="DiscoveryBroadcaster.cs" company="SwarNet">
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
    /// Class DiscoveryBroadcaster.
    /// </summary>
    public class DiscoveryBroadcaster
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
        /// The discovery port
        /// </summary>
        private readonly int _discoveryPort = 55556;
        /// <summary>
        /// The identifier
        /// </summary>
        private readonly string _identifier = "BATTLESHIP_HOST";

        /// <summary>
        /// Starts broadcasting the server's presence every 5 seconds.
        /// </summary>
        /// <param name="tcpGamePort">The TCP port the game server is listening on</param>
        /// <param name="hostName">The hostname/computer name to include in the broadcast</param>
        public void StartBroadcast(int tcpGamePort, string hostName)
        {
            try
            {
                _udpClient = new UdpClient();
                _udpClient.EnableBroadcast = true;
                _running = true;

                string message = $"{_identifier}|{tcpGamePort}|{hostName}";

                Thread broadcastThread = new Thread(() =>
                {
                    IPEndPoint broadcastEndpoint = new IPEndPoint(IPAddress.Broadcast, _discoveryPort);

                    while (_running)
                    {
                        try
                        {
                            byte[] data = Encoding.UTF8.GetBytes(message);
                            _udpClient.Send(data, data.Length, broadcastEndpoint);
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
            _running = false;
            _udpClient?.Close();
        }
    }
}
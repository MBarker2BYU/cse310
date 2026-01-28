// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="NetworkMessage.cs" company="SwarNet">
//     Copyright (c) ShadowWorx Systems, LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using SwarNet.Enums;
using SwarNet.Extensions;
using SwarNet.Serialization;
using SwarNet.Structs;

namespace SwarNet.Networking
{
    /// <summary>
    /// Core message class used for all network communication in SwarNet.
    /// Format on the wire: "MessageType:payload\n"
    /// Example: "ChatMessage:Hello captain!\n"
    /// </summary>
    public class NetworkMessage
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public MessageType Type { get; set; }
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        public string Payload { get; set; } = string.Empty;

        /// <summary>
        /// Parse a received line into a NetworkMessage object
        /// </summary>
        /// <param name="raw">The raw.</param>
        /// <returns>NetworkMessage.</returns>
        /// <exception cref="System.ArgumentException">Cannot parse empty message</exception>
        /// <exception cref="System.ArgumentException">Unknown message type: {parts[0]}</exception>
        public static NetworkMessage FromString(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                throw new ArgumentException("Cannot parse empty message");

            var parts = raw.Split([':'], 2);

            if (!Enum.TryParse<MessageType>(parts[0], ignoreCase: true, out var type))
            {
                throw new ArgumentException($"Unknown message type: {parts[0]}");
            }

            return new NetworkMessage
            {
                Type = type,
                Payload = parts.Length > 1 ? parts[1].Trim() : string.Empty
            };
        }

        /// <summary>
        /// Convert to network-ready string (without the trailing newline)
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return string.IsNullOrEmpty(Payload)
                ? Type.ToString()
                : $"{Type}:{Payload}";
        }

        // Convenience factory methods
        /// <summary>
        /// Chats the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage Chat(string text)
            => new NetworkMessage { Type = MessageType.ChatMessage, Payload = text };

        /// <summary>
        /// Connects the accepted.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage ConnectAccepted(string message)
            => new NetworkMessage { Type = MessageType.ConnectAccepted, Payload = message };

        /// <summary>
        /// Attacks the specified coordinates.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage Attack(GridCell coordinates)
            => new NetworkMessage { Type = MessageType.Attack, Payload = coordinates.ToPayload() };
        
        /// <summary>
        /// Hits the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage HitMessage(string message = "It's a hit!")
            => new NetworkMessage { Type = MessageType.Hit, Payload = message };

        /// <summary>
        /// Misses the message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage MissMessage(string message = "It's a miss!")
            => new NetworkMessage { Type = MessageType.Miss, Payload = message };

        /// <summary>
        /// Games the over.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage GameOver(string message)
            => new NetworkMessage { Type = MessageType.GameOver, Payload = message };

        /// <summary>
        /// Players the ready.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage PlayerReady(Player player)
            => new NetworkMessage { Type = MessageType.Ready, Payload = player.ToPayload()};

        /// <summary>
        /// Ships the sunk.
        /// </summary>
        /// <param name="shipType">Type of the ship.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage ShipSunk(ShipType shipType)
            => new NetworkMessage { Type = MessageType.Sunk, Payload = shipType.ToString() };

        /// <summary>
        /// Places the ships.
        /// </summary>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage PlaceShips(BattleFieldSITREP sitrep)
            => new NetworkMessage { Type = MessageType.PlaceShips, Payload = sitrep.ToPayload()};

        /// <summary>
        /// Sitreps the specified sitrep.
        /// </summary>
        /// <param name="sitrep">The sitrep.</param>
        /// <returns>NetworkMessage.</returns>
        public static NetworkMessage SITREP(BattleFieldSITREP sitrep)
            => new NetworkMessage { Type = MessageType.SITREP, Payload = sitrep.ToPayload() };

    }
}
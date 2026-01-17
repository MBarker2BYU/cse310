// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-17-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="MessageType.cs" company="SwarNet">
//     Copyright (c) ShadowWorx Systems, LLC. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SwarNet.Enums;

/// <summary>
/// Enum MessageType
/// </summary>
internal enum MessageType
{
    /// <summary>
    /// The connect request
    /// </summary>
    ConnectRequest,
    /// <summary>
    /// The connect accepted
    /// </summary>
    ConnectAccepted,
    /// <summary>
    /// The place ships
    /// </summary>
    PlaceShips,
    /// <summary>
    /// The attack
    /// </summary>
    Attack,
    /// <summary>
    /// The hit
    /// </summary>
    Hit,
    /// <summary>
    /// The miss
    /// </summary>
    Miss,
    /// <summary>
    /// The sunk
    /// </summary>
    Sunk,
    /// <summary>
    /// The game over
    /// </summary>
    GameOver,
    /// <summary>
    /// The chat
    /// </summary>
    Chat
}
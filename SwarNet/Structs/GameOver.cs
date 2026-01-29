// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-28-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="GameOver.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;

namespace SwarNet.Structs;

/// <summary>
/// Struct GameOver
/// </summary>
public struct GameOver
{
    /// <summary>
    /// Gets the winner.
    /// </summary>
    /// <value>The winner.</value>
    public Player Winner { get; init; }
    /// <summary>
    /// Gets the loser.
    /// </summary>
    /// <value>The loser.</value>
    public Player Loser { get; init; }
}
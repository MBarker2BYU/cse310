// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-27-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="SITREP.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;

namespace SwarNet.Structs;

/// <summary>
/// Struct SITREP
/// </summary>
/// <param name="player">The player.</param>
/// <param name="hits">The hits.</param>
/// <param name="misses">The misses.</param>
/// <param name="sunk">The sunk.</param>
/// <param name="fleet">The fleet.</param>
/// <param name="damage">The damage.</param>
public readonly struct SITREP(Player player, GridCell[] hits, GridCell[] misses, ShipInfo[] sunk, ShipInfo[] fleet, GridCell[] damage)
{
    /// <summary>
    /// Gets the player.
    /// </summary>
    /// <value>The player.</value>
    public Player Player { get; } = player;

    //Attack Info
    /// <summary>
    /// Gets the hits.
    /// </summary>
    /// <value>The hits.</value>
    public GridCell[] Hits { get; } = hits;
    /// <summary>
    /// Gets the misses.
    /// </summary>
    /// <value>The misses.</value>
    public GridCell[] Misses { get; } = misses;
    /// <summary>
    /// Gets the sunk.
    /// </summary>
    /// <value>The sunk.</value>
    public ShipInfo[] Sunk { get; } = sunk;

    //Fleet Info

    /// <summary>
    /// Gets the fleet.
    /// </summary>
    /// <value>The fleet.</value>
    public ShipInfo[] Fleet { get; } = fleet;
    /// <summary>
    /// Gets the damage.
    /// </summary>
    /// <value>The damage.</value>
    public GridCell[] Damage { get; } = damage;
}
// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-27-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="BattleFieldSITREP.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;

namespace SwarNet.Structs;

/// <summary>
/// Struct BattleFieldSITREP
/// </summary>
/// <param name="playersTurn">The players turn.</param>
/// <param name="reportTo">The report to.</param>
/// <param name="reportOn">The report on.</param>
/// <param name="report">The report.</param>
public readonly struct BattleFieldSITREP(Player playersTurn,  SITREP reportTo , SITREP? reportOn, ShotReport report = ShotReport.None)
{
    /// <summary>
    /// Gets the players turn.
    /// </summary>
    /// <value>The players turn.</value>
    public Player PlayersTurn { get; init; } = playersTurn;

    /// <summary>
    /// Gets the player.
    /// </summary>
    /// <value>The player.</value>
    public Player Player { get; init; } = reportTo.Player;

    //Attack Info
    /// <summary>
    /// Gets the hits.
    /// </summary>
    /// <value>The hits.</value>
    public GridCell[] Hits { get; init; } = reportTo.Hits;
    /// <summary>
    /// Gets the misses.
    /// </summary>
    /// <value>The misses.</value>
    public GridCell[] Misses { get; init; } = reportTo.Misses;
    /// <summary>
    /// Gets the enemy sunk.
    /// </summary>
    /// <value>The enemy sunk.</value>
    public ShipInfo[]? EnemySunk { get; init; } = reportOn?.Sunk;

    //Fleet Info

    /// <summary>
    /// Gets the fleet.
    /// </summary>
    /// <value>The fleet.</value>
    public ShipInfo[] Fleet { get; init; } = reportTo.Fleet;
    /// <summary>
    /// Gets the damage.
    /// </summary>
    /// <value>The damage.</value>
    public GridCell[] Damage { get; init; } = reportTo.Damage;

    /// <summary>
    /// Gets the shot report.
    /// </summary>
    /// <value>The shot report.</value>
    public ShotReport ShotReport { get; init; } = report;
}
// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="GameModule.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;
using SwarNet.Models;
using SwarNet.Structs;

namespace SwarNet.GameLogic;

/// <summary>
/// Class GameModule.
/// </summary>
public class GameModule
{

    /// <summary>
    /// The m random
    /// </summary>
    private static readonly Random m_Random = new Random();

    #region Methods

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="GameModule"/> class.
    /// </summary>
    /// <param name="gridSize">Size of the grid.</param>
    public GameModule(int gridSize)
    {
        GridSize = gridSize;
        
        m_PlayersFleet = [new Fleet(Player.Player1, GridSize), new Fleet(Player.Player2, GridSize)];
    }

    #endregion

    /// <summary>
    /// Deploys the fleets.
    /// </summary>
    public void DeployFleets()
    {
        foreach (var fleet in m_PlayersFleet)
            DeployTheFleet(fleet.Player, true); 
    }

    /// <summary>
    /// Deploys the fleet.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="sitrepOverride">if set to <c>true</c> [sitrep override].</param>
    /// <returns>BattleFieldSITREP.</returns>
    public BattleFieldSITREP DeployTheFleet(Player player, bool sitrepOverride = false)
    {
        m_PlayersFleet[(int)player].AutoDeployFleet();

        return new BattleFieldSITREP(PlayersTurn, m_PlayersFleet[(int)player].GetSITREP(), null);
    }

    /// <summary>
    /// Incomings the specified player.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <param name="gridCell">The grid cell.</param>
    /// <returns>System.ValueTuple{BattleFieldSITREP, BattleFieldSITREP}.</returns>
    public (BattleFieldSITREP Player1SITREP, BattleFieldSITREP Player2SITREP) Incoming(Player player, GridCell gridCell)
    {
        var offense = (int)player;
        var defense = (int)(player == Player.Player1 ? Player.Player2 : Player.Player1);

        var hit = m_PlayersFleet[defense].Incoming(gridCell)
            ? ShotReport.Hit
            : ShotReport.Miss;

        m_PlayersFleet[offense].OutgoingReport(gridCell, hit);

        PlayersTurn = PlayersTurn == Player.Player1 ? Player.Player2 : Player.Player1;

        return GetBattleFieldSITREP(hit);
    }

    /// <summary>
    /// Gets the battle field sitrep.
    /// </summary>
    /// <param name="report">The report.</param>
    /// <returns>System.ValueTuple{BattleFieldSITREP, BattleFieldSITREP}.</returns>
    public (BattleFieldSITREP Player1SITREP, BattleFieldSITREP Player2SITREP) GetBattleFieldSITREP(ShotReport report = ShotReport.None)
    {
        var p1SITREP = m_PlayersFleet[(int)Player.Player1].GetSITREP();
        var p2SITREP = m_PlayersFleet[(int)Player.Player2].GetSITREP();

        return (new BattleFieldSITREP(PlayersTurn, p1SITREP, p2SITREP, report), new BattleFieldSITREP(PlayersTurn, p2SITREP, p1SITREP, report));
    }

    #endregion

    #region Properties and Fields


    /// <summary>
    /// The m players fleet
    /// </summary>
    private readonly Fleet[] m_PlayersFleet;
    /// <summary>
    /// Gets the size of the grid.
    /// </summary>
    /// <value>The size of the grid.</value>
    public int GridSize { get; }

    /// <summary>
    /// Gets the players turn.
    /// </summary>
    /// <value>The players turn.</value>
    public Player PlayersTurn { get; private set; } = Player.Player1;

    #endregion
}
// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="Fleet.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;
using SwarNet.Structs;

namespace SwarNet.Models;

/// <summary>
/// Class Fleet.
/// </summary>
/// <param name="player">The player.</param>
/// <param name="gridSize">Size of the grid.</param>
public class Fleet(Player player, int gridSize)
{
    /// <summary>
    /// The m ships
    /// </summary>
    private readonly List<Ship> m_Ships = [];
    /// <summary>
    /// The m hits
    /// </summary>
    private readonly List<GridCell> m_Hits = [];
    /// <summary>
    /// The m misses
    /// </summary>
    private readonly List<GridCell> m_Misses = [];

    /// <summary>
    /// The m random
    /// </summary>
    private static readonly Random m_Random = new Random();

    /// <summary>
    /// Automatics the deploy fleet.
    /// </summary>
    public void AutoDeployFleet()
    {
        Reset();

        var shipTypes = new[] { ShipType.Carrier, ShipType.Battleship, ShipType.Cruiser, ShipType.Submarine, ShipType.Destroyer };
        var shipIndex = 0;

        while (m_Ships.Count < 5)
        {

            var gridCell = GetRandomGridCell();
            var isHorizontal = GetRandomIsHorizontal();

            var ship = new Ship(shipTypes[shipIndex], gridCell, isHorizontal ? Orientation.Horizontal : Orientation.Vertical);

            if (!PlaceShip(ship))
                continue;

            shipIndex++;

        }
    }

    /// <summary>
    /// Resets this instance.
    /// </summary>
    public void Reset()
    {
        m_Ships.Clear();
        m_Hits.Clear();
        m_Misses.Clear();
    }

    /// <summary>
    /// Places the ship.
    /// </summary>
    /// <param name="ship">The ship.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool PlaceShip(Ship ship)
    {
        if (!IsValidPlacement(ship))
            return false;

        m_Ships.Add(ship);

        return true;
    }


    /// <summary>
    /// Determines whether [is valid placement] [the specified ship].
    /// </summary>
    /// <param name="ship">The ship.</param>
    /// <returns><c>true</c> if [is valid placement] [the specified ship]; otherwise, <c>false</c>.</returns>
    private bool IsValidPlacement(Ship ship)
    {

        if (ship.Location.Length <= 0)
            return false;

        foreach (var gridCell in ship.Location)
        {
            if (gridCell.Row < 0 || gridCell.Row >= GridSize ||
                gridCell.Column < 0 || gridCell.Column >= GridSize)
            {
                return false;
            }
        }

        return m_Ships.All(fleetShip => !fleetShip.Location.Any(gridCell => ship.Location.Contains(gridCell)));
    }

    /// <summary>
    /// Incomings the specified grid cell.
    /// </summary>
    /// <param name="gridCell">The grid cell.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool Incoming(GridCell gridCell)
    {
        foreach (var ship in m_Ships.Where(ship => ship.HitTest(gridCell)))
        {
            ship.RegisterHit(gridCell);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Outgoings the report.
    /// </summary>
    /// <param name="gridCell">The grid cell.</param>
    /// <param name="shotReport">The shot report.</param>
    public void OutgoingReport(GridCell gridCell, ShotReport shotReport)
    {
        switch (shotReport)
        {
            case ShotReport.Hit:
            {
                if(m_Hits.Contains(gridCell))
                    return;

                m_Hits.Add(gridCell);

                break;
            }
            case ShotReport.Miss:
            {
                if(m_Misses.Contains(gridCell))
                    return;

                m_Misses.Add(gridCell);

                break;
            }
            default:
                return;
        }


    }

    /// <summary>
    /// Gets the sitrep.
    /// </summary>
    /// <returns>SITREP.</returns>
    public SITREP GetSITREP()
    {
        var theFleet = new List<ShipInfo>();
        var damage = new List<GridCell>();
        var sunk = new List<ShipInfo>();

        foreach (var ship in m_Ships)
        {
            theFleet.Add(ship.GetShipInfo());
            damage.AddRange(ship.HitCells);
            if(ship.IsSunk)
                sunk.Add(ship.GetShipInfo());
        }

        return new SITREP(Player, m_Hits.ToArray(), m_Misses.ToArray(), sunk.ToArray(), theFleet.ToArray(), damage.ToArray());
    }

    #region Static

    /// <summary>
    /// Gets the random grid cell.
    /// </summary>
    /// <param name="upperBound">The upper bound.</param>
    /// <returns>GridCell.</returns>
    public static GridCell GetRandomGridCell(int upperBound = 10)
    {
        var x = m_Random.Next(upperBound);
        var y = m_Random.Next(upperBound);

        return new GridCell(x, y);
    }

    /// <summary>
    /// Gets the random is horizontal.
    /// </summary>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public static bool GetRandomIsHorizontal()
    {
        return m_Random.Next(2) == 0;
    }

    #endregion

    /// <summary>
    /// Gets the player.
    /// </summary>
    /// <value>The player.</value>
    public Player Player { get; } = player;
    /// <summary>
    /// Gets the size of the grid.
    /// </summary>
    /// <value>The size of the grid.</value>
    public int GridSize { get; } = gridSize;
}
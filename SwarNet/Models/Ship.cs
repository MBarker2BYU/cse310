// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="Ship.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;
using SwarNet.Extensions;
using SwarNet.Structs;

namespace SwarNet.Models;

/// <summary>
/// Class Ship.
/// </summary>
public class Ship
{

    #region Methods

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="Ship"/> class.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="gridCell">The grid cell.</param>
    /// <param name="orientation">The orientation.</param>
    public Ship(ShipType type, GridCell gridCell, Orientation orientation = Orientation.Vertical) 
    {
        Type = type;
        GridCell = gridCell;
        Orientation = orientation;

        Length = Type.GetShipLength();
        m_Cells = GetGridCellsPlus(Length, GridCell, Orientation);
        Location = m_Cells.Keys.ToArray();
    }

    #endregion

    /// <summary>
    /// Gets the ship information.
    /// </summary>
    /// <returns>ShipInfo.</returns>
    public ShipInfo GetShipInfo()
        => new ShipInfo { Type = Type, Location = Location.ToArray() };

    /// <summary>
    /// Hits the test.
    /// </summary>
    /// <param name="gridCell">The grid cell.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool HitTest(GridCell gridCell)
    {
        return Location.Contains(gridCell);
    }

    /// <summary>
    /// Registers the hit.
    /// </summary>
    /// <param name="gridCell">The grid cell.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    public bool RegisterHit(GridCell gridCell)
    {
        if (!Location.Contains(gridCell))
            return false;

        m_Cells[gridCell] = true;
        return true;

    }

    #region Static

    /// <summary>
    /// Gets the grid cells.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <param name="gridCell">The grid cell.</param>
    /// <param name="orientation">The orientation.</param>
    /// <returns>List{GridCell}.</returns>
    public static List<GridCell> GetGridCells(int length, GridCell gridCell,  Orientation orientation = Orientation.Vertical)
    {
        var cells = new List<GridCell>();

        var isHorizontal = orientation == Orientation.Horizontal;

        for (var index = 0; index < length; index++)
        {
            var row = isHorizontal ? gridCell.Row : gridCell.Row + index;
            var column = isHorizontal ? gridCell.Column + index : gridCell.Column;

            cells.Add(new GridCell(row, column));
        }

        return cells;
    }

    /// <summary>
    /// Gets the grid cells plus.
    /// </summary>
    /// <param name="length">The length.</param>
    /// <param name="gridCell">The grid cell.</param>
    /// <param name="orientation">The orientation.</param>
    /// <returns>Dictionary{GridCell, System.Boolean}.</returns>
    public static Dictionary<GridCell, bool> GetGridCellsPlus(int length, GridCell gridCell, Orientation orientation = Orientation.Vertical)
    {
        return GetGridCells(length, gridCell, orientation).ToDictionary(cell => cell, cell => false);
    }

    #endregion

    #endregion

    #region Properties and Fields

    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <value>The type.</value>
    public ShipType Type { get; }
    /// <summary>
    /// Gets the location.
    /// </summary>
    /// <value>The location.</value>
    public GridCell[] Location { get;  private init; }

    /// <summary>
    /// Gets the grid cell.
    /// </summary>
    /// <value>The grid cell.</value>
    public GridCell GridCell { get; }
    /// <summary>
    /// Gets the orientation.
    /// </summary>
    /// <value>The orientation.</value>
    public Orientation Orientation { get; }

    /// <summary>
    /// Gets the length.
    /// </summary>
    /// <value>The length.</value>
    public int Length { get; }
    /// <summary>
    /// Gets a value indicating whether this instance is sunk.
    /// </summary>
    /// <value><c>true</c> if this instance is sunk; otherwise, <c>false</c>.</value>
    public bool IsSunk => m_Cells.Values.All(isHit => isHit);
    /// <summary>
    /// Gets the hits.
    /// </summary>
    /// <value>The hits.</value>
    public int Hits => m_Cells.Values.Count(isHit => isHit);
    /// <summary>
    /// Gets the cells remaining.
    /// </summary>
    /// <value>The cells remaining.</value>
    public int CellsRemaining => Length - Hits;

    /// <summary>
    /// Gets the hit cells.
    /// </summary>
    /// <value>The hit cells.</value>
    public IEnumerable<GridCell> HitCells =>
        m_Cells.Where(kv => kv.Value).Select(kv => kv.Key);

    /// <summary>
    /// The m cells
    /// </summary>
    private readonly Dictionary<GridCell, bool> m_Cells;

    #endregion
}
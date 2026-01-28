using SwarNet.Enums;
using SwarNet.Extensions;
using SwarNet.Structs;

namespace SwarNet.Models;

public class Ship
{

    #region Methods

    #region Constructors

    public Ship(ShipType type, GridCell gridCell, Orientation orientation = Orientation.Vertical) 
    {
        GridCell = gridCell;
        Orientation = orientation;

        Length = Type.GetShipLength();
        m_Cells = GetGridCellsPlus(Length, GridCell, Orientation);
        Location = m_Cells.Keys.ToArray();
    }

    #endregion

    public ShipInfo GetShipInfo()
        => new ShipInfo { Type = Type, Location = Location.ToArray() };

    public bool HitTest(GridCell gridCell)
    {
        return Location.Contains(gridCell);
    }

    public bool RegisterHit(GridCell gridCell)
    {
        if (!Location.Contains(gridCell))
            return false;

        m_Cells[gridCell] = true;
        return true;

    }

    #region Static

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

    public static Dictionary<GridCell, bool> GetGridCellsPlus(int length, GridCell gridCell, Orientation orientation = Orientation.Vertical)
    {
        return GetGridCells(length, gridCell, orientation).ToDictionary(cell => cell, cell => false);
    }

    #endregion

    #endregion

    #region Properties and Fields

    public ShipType Type { get; }
    public GridCell[] Location { get;  private init; }

    public GridCell GridCell { get; } 
    public Orientation Orientation { get; }
    
    public int Length { get; }
    public bool IsSunk => m_Cells.Values.All(isHit => isHit);
    public int Hits => m_Cells.Values.Count(isHit => isHit);
    public int CellsRemaining => Length - Hits;

    public IEnumerable<GridCell> HitCells =>
        m_Cells.Where(kv => kv.Value).Select(kv => kv.Key);

    private readonly Dictionary<GridCell, bool> m_Cells;

    #endregion
}
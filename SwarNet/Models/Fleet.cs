using SwarNet.Enums;
using SwarNet.Structs;

namespace SwarNet.Models;

public class Fleet(Player player, int gridSize)
{
    private readonly List<Ship> m_Ships = [];
    private readonly List<GridCell> m_Hits = [];
    private readonly List<GridCell> m_Misses = [];

    private static readonly Random m_Random = new Random();

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

    public void Reset()
    {
        m_Ships.Clear();
        m_Hits.Clear();
        m_Misses.Clear();
    }

    public bool PlaceShip(Ship ship)
    {
        if (!IsValidPlacement(ship))
            return false;

        m_Ships.Add(ship);

        return true;
    }


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

    public bool Incoming(GridCell gridCell)
    {
        foreach (var ship in m_Ships.Where(ship => ship.HitTest(gridCell)))
        {
            ship.RegisterHit(gridCell);
            return true;
        }

        return false;
    }

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

    public static GridCell GetRandomGridCell(int upperBound = 10)
    {
        var x = m_Random.Next(upperBound);
        var y = m_Random.Next(upperBound);

        return new GridCell(x, y);
    }

    public static bool GetRandomIsHorizontal()
    {
        return m_Random.Next(2) == 0;
    }
    
    #endregion

    public Player Player { get; } = player;
    public int GridSize { get; } = gridSize;
}
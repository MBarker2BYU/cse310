using SwarNet.Enums;
using SwarNet.Models;

namespace SwarNet.Structs;

public readonly struct SITREP(Player player, GridCell[] hits, GridCell[] misses, ShipInfo[] sunk, ShipInfo[] fleet, GridCell[] damage)
{
    public Player Player { get; } = player;

    //Attack Info
    public GridCell[] Hits { get; } = hits;
    public GridCell[] Misses { get; } = misses;
    public ShipInfo[] Sunk { get; } = sunk;

    //Fleet Info

    public ShipInfo[] Fleet { get; } = fleet;
    public GridCell[] Damage { get; } = damage;
}
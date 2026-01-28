using SwarNet.Enums;
using SwarNet.Models;

namespace SwarNet.Structs;

public readonly struct BattleFieldSITREP(Player playersTurn,  SITREP reportTo , SITREP? reportOn)
{
    public Player PlayersTurn { get; init; } = playersTurn;

    public Player Player { get; init; } = reportTo.Player;

    //Attack Info
    public GridCell[] Hits { get; init; } = reportTo.Hits;
    public GridCell[] Misses { get; init; } = reportTo.Misses;
    public ShipInfo[]? EnemySunk { get; init; } = reportOn?.Sunk;

    //Fleet Info

    public ShipInfo[] Fleet { get; init; } = reportTo.Fleet;
    public GridCell[] Damage { get; init; } = reportTo.Damage;
}
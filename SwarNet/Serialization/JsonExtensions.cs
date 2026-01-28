using System.Text.Json;
using SwarNet.Enums;
using SwarNet.Structs;

namespace SwarNet.Serialization;

public static class JsonExtensions
{
    public static string ToPayload(this GridCell gridCell)
    {
        return JsonSerializer.Serialize(gridCell);
    }

    public static string ToPayload(this ShipInfo shipInfo)
    {
        return JsonSerializer.Serialize(shipInfo);
    }

    public static string ToPayload(this BattleFieldSITREP sitrep)
    {
        return JsonSerializer.Serialize(sitrep);
    }

    public static string ToPayload(this Player player)
    {
        return JsonSerializer.Serialize(player);
    }

    public static GridCell ToGridCell(this string payload)
    {
        return JsonSerializer.Deserialize<GridCell>(payload);
    }

    public static ShipInfo ToShipInfo(this string payload)
    {
        return JsonSerializer.Deserialize<ShipInfo>(payload);
    }

    public static BattleFieldSITREP ToBattleFieldSITREP(this string payload)
    {
        return JsonSerializer.Deserialize<BattleFieldSITREP>(payload);
    }

    public static Player ToPlayer(this string payload)
    {
        return JsonSerializer.Deserialize<Player>(payload);
    }
}
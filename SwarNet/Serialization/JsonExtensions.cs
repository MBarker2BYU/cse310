// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-28-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="JsonExtensions.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Text.Json;
using SwarNet.Enums;
using SwarNet.Models;
using SwarNet.Structs;

namespace SwarNet.Serialization;

/// <summary>
/// Class JsonExtensions.
/// </summary>
public static class JsonExtensions
{
    /// <summary>
    /// Converts to payload.
    /// </summary>
    /// <param name="gridCell">The grid cell.</param>
    /// <returns>System.String.</returns>
    public static string ToPayload(this GridCell gridCell)
    {
        return JsonSerializer.Serialize(gridCell);
    }

    /// <summary>
    /// Converts to payload.
    /// </summary>
    /// <param name="shipInfo">The ship information.</param>
    /// <returns>System.String.</returns>
    public static string ToPayload(this ShipInfo shipInfo)
    {
        return JsonSerializer.Serialize(shipInfo);
    }

    /// <summary>
    /// Converts to payload.
    /// </summary>
    /// <param name="sitrep">The sitrep.</param>
    /// <returns>System.String.</returns>
    public static string ToPayload(this BattleFieldSITREP sitrep)
    {
        return JsonSerializer.Serialize(sitrep);
    }

    /// <summary>
    /// Converts to payload.
    /// </summary>
    /// <param name="player">The player.</param>
    /// <returns>System.String.</returns>
    public static string ToPayload(this Player player)
    {
        return JsonSerializer.Serialize(player);
    }

    /// <summary>
    /// Converts to payload.
    /// </summary>
    /// <param name="resources">The resources.</param>
    /// <returns>System.String.</returns>
    public static string ToPayload(this TextResources resources)
    {
        return JsonSerializer.Serialize(resources);
    }

    /// <summary>
    /// Converts to gridcell.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>GridCell.</returns>
    public static GridCell ToGridCell(this string payload)
    {
        return JsonSerializer.Deserialize<GridCell>(payload);
    }

    /// <summary>
    /// Converts to shipinfo.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>ShipInfo.</returns>
    public static ShipInfo ToShipInfo(this string payload)
    {
        return JsonSerializer.Deserialize<ShipInfo>(payload);
    }

    /// <summary>
    /// Converts to battlefieldsitrep.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>BattleFieldSITREP.</returns>
    public static BattleFieldSITREP ToBattleFieldSITREP(this string payload)
    {
        return JsonSerializer.Deserialize<BattleFieldSITREP>(payload);
    }

    /// <summary>
    /// Converts to player.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>Player.</returns>
    public static Player ToPlayer(this string payload)
    {
        return JsonSerializer.Deserialize<Player>(payload);
    }

    /// <summary>
    /// Converts to textresources.
    /// </summary>
    /// <param name="payload">The payload.</param>
    /// <returns>TextResources.</returns>
    public static TextResources ToTextResources(this string payload)
    {
        return JsonSerializer.Deserialize<TextResources>(payload)!;
    }
}
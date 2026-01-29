// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="EnumExtensions.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Attributes;

namespace SwarNet.Extensions;

/// <summary>
/// Class EnumExtensions.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the length of the ship.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>System.Int32.</returns>
    public static int GetShipLength(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());
        if (field == null) return 0;

        var attr = (ShipLengthAttribute?)Attribute.GetCustomAttribute(
            field, typeof(ShipLengthAttribute));

        return attr?.Value ?? 0;
    }
}
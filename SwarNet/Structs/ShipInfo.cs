// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-27-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-27-2026
// ***********************************************************************
// <copyright file="ShipInfo.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// *********************************************************************** 

using SwarNet.Enums;

namespace SwarNet.Structs;

/// <summary>
/// Struct ShipInfo
/// </summary>
public readonly struct ShipInfo
{
    /// <summary>
    /// Gets the type.
    /// </summary>
    /// <value>The type.</value>
    public ShipType Type { get; init; }
    /// <summary>
    /// Gets the location.
    /// </summary>
    /// <value>The location.</value>
    public GridCell[] Location { get; init; }
}
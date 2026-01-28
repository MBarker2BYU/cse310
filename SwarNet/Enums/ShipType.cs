// ***********************************************************************
// Assembly        : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="ShipType.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using SwarNet.Attributes;

namespace SwarNet.Enums;

/// <summary>
/// Enum ShipType
/// </summary>
public enum ShipType
{
    /// <summary>
    /// The carrier
    /// </summary>
    [ShipLength(5)]
    Carrier,
    /// <summary>
    /// The battleship
    /// </summary>
    [ShipLength(4)]
    Battleship,
    /// <summary>
    /// The cruiser
    /// </summary>
    [ShipLength(3)]
    Cruiser,
    /// <summary>
    /// The submarine
    /// </summary>
    [ShipLength(3)]
    Submarine,
    /// <summary>
    /// The destroyer
    /// </summary>
    [ShipLength(2)]
    Destroyer
}
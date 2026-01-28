// ***********************************************************************
// Assembly        : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="ShipLengthAttribute.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace SwarNet.Attributes;

/// <summary>
/// Class ShipLengthAttribute.
/// Implements the <see cref="System.Attribute" />
/// </summary>
/// <param name="value">The value.</param>
/// <seealso cref="System.Attribute" />
[AttributeUsage(AttributeTargets.Field)]
public class ShipLengthAttribute(int value) : Attribute
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public int Value { get; } = value;
}
// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-27-2026
// ***********************************************************************
// <copyright file="GridCell.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace SwarNet.Structs;

/// <summary>
/// Struct GridCell
/// Implements the <see cref="System.IEquatable{SwarNet.Structs.GridCell}" />
/// </summary>
/// <param name="row">The row.</param>
/// <param name="column">The column.</param>
/// <seealso cref="System.IEquatable{SwarNet.Structs.GridCell}" />
public readonly struct GridCell(int row, int column) : IEquatable<GridCell>
{

    #region Properties

    /// <summary>
    /// Gets the row.
    /// </summary>
    /// <value>The row.</value>
    public int Row { get; init; } = row;
    /// <summary>
    /// Gets the column.
    /// </summary>
    /// <value>The column.</value>
    public int Column { get; init; } = column;

    #endregion

    #region IEquatable

    // Allow easy conversion to Point if needed (e.g. for drawing)
    /// <summary>
    /// Performs an implicit conversion from <see cref="GridCell"/> to <see cref="Point"/>.
    /// </summary>
    /// <param name="cell">The cell.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator Point(GridCell cell)
        => new Point(cell.Column, cell.Row);

    /// <summary>
    /// Performs an implicit conversion from <see cref="Point"/> to <see cref="GridCell"/>.
    /// </summary>
    /// <param name="p">The p.</param>
    /// <returns>The result of the conversion.</returns>
    public static implicit operator GridCell(Point p)
        => new GridCell(p.Y, p.X);

    /// <summary>
    /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj) 
        => obj is GridCell other && Equals(other);

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
    public bool Equals(GridCell other) 
        => Row == other.Row && Column == other.Column;

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
    public override int GetHashCode() 
        => HashCode.Combine(Row, Column);

    /// <summary>
    /// Returns a <see cref="System.String" /> that represents this instance.
    /// </summary>
    /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
    public override string ToString() 
        => $"R: {Row}, C: {Column}";

    #endregion
}
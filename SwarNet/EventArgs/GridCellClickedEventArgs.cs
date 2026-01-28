// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-26-2026
// ***********************************************************************
// <copyright file="GridCellClickedEventArgs.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using SwarNet.Structs;

namespace SwarNet.EventArgs;

/// <summary>
/// Class GridCellClickedEventArgs.
/// Implements the <see cref="System.EventArgs" />
/// </summary>
/// <param name="gridCell">The grid cell.</param>
/// <param name="button">The button.</param>
/// <param name="clicks">The clicks.</param>
/// <seealso cref="System.EventArgs" />
public class GridCellClickedEventArgs(GridCell gridCell, MouseButtons button, int clicks) : System.EventArgs
{
    /// <summary>
    /// Gets the grid cell.
    /// </summary>
    /// <value>The grid cell.</value>
    public GridCell GridCell { get; } = gridCell;
    /// <summary>
    /// Gets the button.
    /// </summary>
    /// <value>The button.</value>
    public MouseButtons Button { get; } = button;
    /// <summary>
    /// Gets the clicks.
    /// </summary>
    /// <value>The clicks.</value>
    public int Clicks { get; } = clicks;
}
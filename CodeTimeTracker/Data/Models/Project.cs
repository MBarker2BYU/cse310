// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-15-2026
// ***********************************************************************
// <copyright file="Project.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace CodeTimeTracker.Data.Models;

/// <summary>
/// Class Project.
/// </summary>
public class Project
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;  // e.g. "Customer Portal", "Inventory System"
    /// <summary>
    /// Gets or sets the created at.
    /// </summary>
    /// <value>The created at.</value>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
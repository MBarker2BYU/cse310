// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-15-2026
// ***********************************************************************
// <copyright file="CodeObject.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace CodeTimeTracker.Data.Models;

/// <summary>
/// Class CodeObject.
/// </summary>
public class CodeObject
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Gets or sets the project identifier.
    /// </summary>
    /// <value>The project identifier.</value>
    public Guid ProjectId { get; set; }               // foreign key
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;  // e.g. "MainForm", "UserService", "LoginViewModel"
    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    /// <value>The type.</value>
    public string Type { get; set; } = string.Empty;  // optional: "Form", "Class", "UserControl", "Service", etc.
    /// <summary>
    /// Gets or sets the created at.
    /// </summary>
    /// <value>The created at.</value>
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

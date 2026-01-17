// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-15-2026
// ***********************************************************************
// <copyright file="TimeEntry.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace CodeTimeTracker.Data.Models;

/// <summary>
/// Class TimeEntry.
/// </summary>
public class TimeEntry
{
    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public Guid Id { get; set; } = Guid.NewGuid();
    /// <summary>
    /// Gets or sets the code object identifier.
    /// </summary>
    /// <value>The code object identifier.</value>
    public Guid CodeObjectId { get; set; }            // foreign key to CodeObject
    /// <summary>
    /// Gets or sets the name of the task.
    /// </summary>
    /// <value>The name of the task.</value>
    public string TaskName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the start time.
    /// </summary>
    /// <value>The start time.</value>
    public DateTime StartTime { get; set; }
    /// <summary>
    /// Gets or sets the end time.
    /// </summary>
    /// <value>The end time.</value>
    public DateTime? EndTime { get; set; }            // null = currently running
    /// <summary>
    /// Gets or sets the notes.
    /// </summary>
    /// <value>The notes.</value>
    public string Notes { get; set; } = string.Empty; // optional, for extra context

    /// <summary>
    /// Gets or sets a value indicating whether this instance is deleted.
    /// </summary>
    /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
    public bool IsDeleted { get; set; } = false;

    // Computed properties (not stored)
    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <value>The duration.</value>
    public TimeSpan Duration =>
        EndTime.HasValue ? EndTime.Value - StartTime : DateTime.Now - StartTime;

    /// <summary>
    /// Gets the duration formatted.
    /// </summary>
    /// <value>The duration formatted.</value>
    public string DurationFormatted =>
        $"{Duration.Hours:D2}:{Duration.Minutes:D2}:{Duration.Seconds:D2}";
}
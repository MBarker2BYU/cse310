// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="TimeTrackerData.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace CodeTimeTracker.Data.Models
{
    /// <summary>
    /// Class TimeTrackerData.
    /// </summary>
    public class TimeTrackerData
    {
        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        /// <value>The projects.</value>
        public List<Project> Projects { get; set; } = new List<Project>();
        /// <summary>
        /// Gets or sets the code objects.
        /// </summary>
        /// <value>The code objects.</value>
        public List<CodeObject> CodeObjects { get; set; } = new List<CodeObject>();
        /// <summary>
        /// Gets or sets the time entries.
        /// </summary>
        /// <value>The time entries.</value>
        public List<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
    }
}
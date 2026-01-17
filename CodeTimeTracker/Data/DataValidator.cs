// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-16-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-16-2026
// ***********************************************************************
// <copyright file="DataValidator.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using CodeTimeTracker.Data.Models;

namespace CodeTimeTracker.Data
{
    /// <summary>
    /// Class DataValidator.
    /// </summary>
    public static class DataValidator
    {
        /// <summary>
        /// Checks if a Project with the given name already exists (case-insensitive).
        /// </summary>
        /// <param name="name">The project name to check.</param>
        /// <param name="data">The TimeTrackerData containing projects.</param>
        /// <returns>(exists: bool, id: Guid) - exists=true if duplicate found, id=matching Guid or Guid.Empty if not.</returns>
        public static (bool exists, Guid id) ProjectExists(string name, TimeTrackerData data)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, Guid.Empty);

            var existing = data.Projects.FirstOrDefault(p =>
                string.Equals(p.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));

            return (existing != null, existing?.Id ?? Guid.Empty);
        }

        /// <summary>
        /// Checks if a CodeObject with the given name already exists within a specific Project (case-insensitive).
        /// </summary>
        /// <param name="projectId">The Guid of the project to scope the check.</param>
        /// <param name="name">The code object name to check.</param>
        /// <param name="data">The TimeTrackerData containing code objects.</param>
        /// <returns>(exists: bool, id: Guid) - exists=true if duplicate found, id=matching Guid or Guid.Empty if not.</returns>
        public static (bool exists, Guid id) CodeObjectExists(Guid projectId, string name, TimeTrackerData data)
        {
            if (string.IsNullOrWhiteSpace(name))
                return (false, Guid.Empty);

            var existing = data.CodeObjects.FirstOrDefault(co =>
                co.ProjectId == projectId &&
                string.Equals(co.Name, name.Trim(), StringComparison.OrdinalIgnoreCase));

            return (existing != null, existing?.Id ?? Guid.Empty);
        }
    }
}
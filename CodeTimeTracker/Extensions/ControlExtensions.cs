// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-16-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-16-2026
// ***********************************************************************
// <copyright file="ControlExtensions.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using CodeTimeTracker.Data.Models;

namespace CodeTimeTracker.Extensions
{
    /// <summary>
    /// Class ControlExtensions.
    /// </summary>
    public static class ControlExtensions
    {
        /// <summary>
        /// Loads the ComboBox.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combo">The combo.</param>
        /// <param name="data">The data.</param>
        /// <param name="beforeLoad">The before load.</param>
        /// <param name="afterLoad">The after load.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool LoadComboBox<T>(this ComboBox combo, List<T> data , Func<bool>? beforeLoad, Func<bool>? afterLoad)
        {
            try
            {
                combo.Items.Clear();
                combo.Enabled = false;

                if(beforeLoad != null )
                   if(!beforeLoad())
                        return true;

                foreach (T item in data)
                {
                    if(item == null) continue;
                    combo.Items.Add(item);
                }

                return afterLoad == null ? true : afterLoad();
            }
            catch
            {
                combo.Items.Clear();
                combo.Items.Add("Loading Error");

                return false;
            }
            finally 
            {
                combo.Enabled = true;
            }
        }

        /// <summary>
        /// Loads the ComboBox.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="combo">The combo.</param>
        /// <param name="data">The data.</param>
        /// <param name="button">The button.</param>
        /// <param name="beforeLoad">The before load.</param>
        /// <param name="afterLoad">The after load.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool LoadComboBox<T>(this ComboBox combo, List<T> data, Button? button, Func<bool>? beforeLoad, Func<bool>? afterLoad)
        {
            try
            {
                if(button != null)
                    button.Enabled = false;               

                if (!combo.LoadComboBox<T>(data, beforeLoad, afterLoad))
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (button != null)
                    button.Enabled = true;
            }
        }

        /// <summary>
        /// Loads the projects.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="data">The data.</param>
        /// <param name="button">The button.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool LoadProjects(this ComboBox combo, TimeTrackerData data, Button? button = null)
        {
            try
            {
                var projects = data.Projects.OrderBy(p => p.Name).ToList();

                var beforeLoad = () =>
                {
                    if (projects.Count == 0)
                    {
                        combo.Items.Add("(No projects found)");
                        combo.SelectedIndex = 0;
                        combo.Enabled = false;

                        return false;
                    }

                    return true;
                };
                
                var afterLoad = () => 
                {
                    combo.DisplayMember = "Name";

                    combo.SelectedIndex = 0;
                    combo.Enabled = true;

                    return true;
                };

                if (button == null)
                {
                    if(!combo.LoadComboBox(projects, beforeLoad, afterLoad))
                        return false;
                }
                else
                {
                    if (!combo.LoadComboBox(projects, button, beforeLoad, afterLoad))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally 
            {
                
            }
        }

        /// <summary>
        /// Loads the code objects.
        /// </summary>
        /// <param name="combo">The combo.</param>
        /// <param name="data">The data.</param>
        /// <param name="projectCombo">The project combo.</param>
        /// <param name="button">The button.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool LoadCodeObjects(this ComboBox combo, TimeTrackerData data, ComboBox projectCombo, Button? button = null)
        {
            try
            {
                if (projectCombo.SelectedItem == null || !(projectCombo.SelectedItem is Project selectedProject))
                {
                    combo.Items.Clear();
                    combo.Items.Add("Select Project First");
                    combo.SelectedIndex = 0;
                    combo.Enabled= false;

                    return true;
                }


                var codeObjects = data.CodeObjects
                    .Where(co => co.ProjectId == selectedProject.Id)
                    .OrderBy(co => co.Name)
                    .ToList();

                var beforeLoad = () =>
                {
                    if (codeObjects.Count == 0)
                    {
                        combo.Items.Add("(No code objects yet)");
                        combo.SelectedIndex = 0;

                        return false;
                    }

                    return true;
                };

                var afterLoad = () =>
                {
                    combo.DisplayMember = "Name";
                    combo.ValueMember = "Id";

                    combo.SelectedIndex = 0;
                    combo.Enabled = true;

                    return true;
                };

                if (button == null)
                {
                    if (!combo.LoadComboBox(codeObjects, beforeLoad, afterLoad))
                        return false;
                }
                else
                {
                    if (!combo.LoadComboBox(codeObjects, button, beforeLoad, afterLoad))
                        return false;
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {

            }
        }
        
    }
}

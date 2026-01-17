using CodeTimeTracker.Data.Models;
using System.Collections.Generic;

namespace CodeTimeTracker.Extensions
{
    public static class ControlExtensions
    {
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

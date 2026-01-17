// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author            : Matthew D. Barker
// Created           : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-16-2026
// ***********************************************************************
// <copyright file="TimeEntryForm.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using CodeTimeTracker.Data.Models;
using CodeTimeTracker.Extensions;

namespace CodeTimeTracker
{
    /// <summary>
    /// Class TimeEntryForm.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class TimeEntryForm : Form
    {
        /// <summary>
        /// The m data
        /// </summary>
        private readonly TimeTrackerData m_Data;
        /// <summary>
        /// The m existing entry
        /// </summary>
        private TimeEntry? m_ExistingEntry;  // null = new entry mode

        // Constructor for ADD new entry
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryForm"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="preselectProject">The preselect project.</param>
        /// <exception cref="System.ArgumentNullException">data</exception>
        public TimeEntryForm(TimeTrackerData data, Project? preselectProject = null)
        {
            m_Data = data ?? throw new ArgumentNullException(nameof(data));
            InitializeComponent();
            Text = "Add New Time Entry";
            btnSave.Text = "Create";

            LoadProjectsAndCodeObjects(preselectProject);

            startPicker.Value = DateTime.Now;
            endPicker.Value = DateTime.Now.AddSeconds(30);
        }

        // Constructor for EDIT existing entry
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryForm"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="entry">The entry.</param>
        /// <exception cref="System.ArgumentNullException">data</exception>
        /// <exception cref="System.ArgumentNullException">entry</exception>
        public TimeEntryForm(TimeTrackerData data, TimeEntry entry)
        {
            m_Data = data ?? throw new ArgumentNullException(nameof(data));
            m_ExistingEntry = entry ?? throw new ArgumentNullException(nameof(entry));
            InitializeComponent();
            Text = "Edit Time Entry";
            btnSave.Text = "Update";

            LoadProjectsAndCodeObjects();

            // Find and select the correct project/code object
            var codeObj = m_Data.CodeObjects.FirstOrDefault(co => co.Id == entry.CodeObjectId);
            if (codeObj != null)
            {
                var project = m_Data.Projects.FirstOrDefault(p => p.Id == codeObj.ProjectId);
                if (project != null)
                {
                    cmbProject.SelectedItem = project;
                    // Code objects will auto-populate on selection changed
                    cmbCodeObject.SelectedItem = codeObj;
                }
            }

            txtTaskName.Text = entry.TaskName;
            startPicker.Value = entry.StartTime;
            endPicker.Value = entry.EndTime ?? DateTime.Now;
        }

        /// <summary>
        /// Loads the projects and code objects.
        /// </summary>
        /// <param name="preselect">The preselect.</param>
        private void LoadProjectsAndCodeObjects(Project? preselect = null)
        {
            cmbProject.DisplayMember = "Name";
            foreach (var p in m_Data.Projects.OrderBy(p => p.Name))
                cmbProject.Items.Add(p);

            if (preselect != null && cmbProject.Items.Contains(preselect))
                cmbProject.SelectedItem = preselect;
            else if (cmbProject.Items.Count > 0)
                cmbProject.SelectedIndex = 0;

            cmbProject.SelectedIndexChanged += (s, e) => UpdateCodeObjects();
            UpdateCodeObjects();
        }

        /// <summary>
        /// Updates the code objects.
        /// </summary>
        private void UpdateCodeObjects()
        {
            cmbCodeObject.Items.Clear();
            cmbCodeObject.DisplayMember = "Name";

            if (cmbProject.SelectedItem is Project project)
            {
                var codeObjs = m_Data.CodeObjects
                    .Where(co => co.ProjectId == project.Id)
                    .OrderBy(co => co.Name);

                foreach (var co in codeObjs)
                    cmbCodeObject.Items.Add(co);

                if (cmbCodeObject.Items.Count > 0)
                    cmbCodeObject.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCodeObject.SelectedItem == null)
            {
                MessageBox.Show("Select a Code Object.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Enter a Task Name.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime start = startPicker.Value;
            DateTime end = endPicker.Value;

            if (start >= end)
            {
                MessageBox.Show("Start must be before End time.", "Invalid Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CodeObject codeObj = (CodeObject)cmbCodeObject.SelectedItem;

            if (m_ExistingEntry == null)
            {
                // ADD new
                TimeEntry newEntry = new()
                {
                    Id = Guid.NewGuid(),
                    CodeObjectId = codeObj.Id,
                    TaskName = txtTaskName.Text.Trim(),
                    StartTime = start,
                    EndTime = end
                };
                m_Data.TimeEntries.Add(newEntry);
            }
            else
            {
                // EDIT existing
                m_ExistingEntry.CodeObjectId = codeObj.Id;
                m_ExistingEntry.TaskName = txtTaskName.Text.Trim();
                m_ExistingEntry.StartTime = start;
                m_ExistingEntry.EndTime = end;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handles the Click event of the btnNewProject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNewProject_Click(object sender, EventArgs e)
        {

            var newProjectWindow = new NewProjectWindow() { Data = m_Data };
            
            if (newProjectWindow.ShowDialog() != DialogResult.OK  || string.IsNullOrWhiteSpace(newProjectWindow.NewProject?.Name))
                return;

            cmbProject.LoadProjects(m_Data);

            if (newProjectWindow.NewProject != null)
                cmbProject.SelectedItem = newProjectWindow.NewProject;

        }

        /// <summary>
        /// Handles the Click event of the btnNewCodeObject control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNewCodeObject_Click(object sender, EventArgs e)
        {
            if (cmbProject.SelectedItem == null || !(cmbProject.SelectedItem is Project selectedProject))
            {
                MessageBox.Show("Select a project first.", "No Project Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCodeObjectWindow = new NewCodeObjectWindow() { ProjectId = selectedProject.Id, Data = m_Data };

            if (newCodeObjectWindow.ShowDialog() != DialogResult.OK  || string.IsNullOrWhiteSpace(newCodeObjectWindow.NewCodeObject?.Name))
                return;
            
            cmbCodeObject.LoadCodeObjects(m_Data, cmbProject, btnNewCodeObject);

            if(newCodeObjectWindow.NewCodeObject != null)
                cmbCodeObject.SelectedItem = newCodeObjectWindow.NewCodeObject;
            
        }
    }
}
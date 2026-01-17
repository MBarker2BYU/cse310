// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-15-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-16-2026
// ***********************************************************************
// <copyright file="NewProjectWindow.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using CodeTimeTracker.Data;
using CodeTimeTracker.Data.Models;

namespace CodeTimeTracker
{
    /// <summary>
    /// Class NewProjectWindow.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class NewProjectWindow : Form
    {
        #region Methods

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewProjectWindow"/> class.
        /// </summary>
        public NewProjectWindow() 
        {
            InitializeComponent();            

            WireEvents();
        }

        #endregion

        /// <summary>
        /// Wires the events.
        /// </summary>
        private void WireEvents()
        {
            btnCreate.Click += btnCreate_Click!;
        }

        #region Event Handlers

        /// <summary>
        /// Handles the Click event of the btnCreate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCreate_Click(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show("Please enter a Project name.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (m_Data == null)
            {
                MessageBox.Show("Data connection was not provide.", "Invalid Paramters", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;

                return;
            }

            (bool exists, Guid id) = DataValidator.ProjectExists(txtProjectName.Text, m_Data);

            if (exists)
            {
                MessageBox.Show(
                    $"A Project named '{txtProjectName.Text}' already exists.\nPlease choose a different name.",
                    "Duplicate Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            NewProject = new Project { Name = txtProjectName.Text.Trim() };
            m_Data.Projects.Add(NewProject);

            JsonStorage.Save(m_Data);
            
            MessageBox.Show($"Project '{NewProject.Name}' created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            DialogResult = DialogResult.OK;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// The m data
        /// </summary>
        private TimeTrackerData m_Data = null!;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public TimeTrackerData Data
        {
            get { return m_Data; }
            set
            {
                if (m_Data == null)
                    m_Data = value;
            }
        }

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <value>The new project.</value>
        public Project NewProject { private set; get; }

        #endregion
    }
}

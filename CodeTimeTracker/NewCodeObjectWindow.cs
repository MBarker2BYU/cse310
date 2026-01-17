// ***********************************************************************
// Assembly         : CodeTimeTracker
// Author           : Matthew D. Barker
// Created          : 01-16-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-17-2026
// ***********************************************************************
// <copyright file="NewCodeObjectWindow.cs" company="ShadowWorx Systems">
//     Copyright © 2026 Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using CodeTimeTracker.Data;
using CodeTimeTracker.Data.Models;

namespace CodeTimeTracker
{
    /// <summary>
    /// Class NewCodeObjectWindow.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class NewCodeObjectWindow : Form
    {

        /// <summary>
        /// The m project identifier
        /// </summary>
        private Guid m_ProjectId = Guid.Empty;

        /// <summary>
        /// Gets or sets the project identifier.
        /// </summary>
        /// <value>The project identifier.</value>
        public Guid ProjectId
        {
            get { return m_ProjectId; }
            set
            {
                if (m_ProjectId == Guid.Empty)
                    m_ProjectId = value;
            }
        }

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
        /// Initializes a new instance of the <see cref="NewCodeObjectWindow"/> class.
        /// </summary>
        public NewCodeObjectWindow()
        {
            InitializeComponent();
            InitializeUI();
        }

        /// <summary>
        /// Wires the events.
        /// </summary>
        private void WireEvents()
        { 
            btnCreate.Click += btnCreate_Click!;
        }

        /// <summary>
        /// Initializes the UI.
        /// </summary>
        private void InitializeUI()
        {
            cmbCodeObjectType.Items.AddRange(new[] { "Document","Research","Form", "Class", "UserControl", "Service", "ViewModel", "Component", "Controller", "Model", "View", "" });
            cmbCodeObjectType.SelectedIndex = cmbCodeObjectType.Items.Count - 1;

            WireEvents();
        }

        /// <summary>
        /// Handles the Click event of the btnCreate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCreate_Click(object sender, EventArgs args)
        {
            if (string.IsNullOrWhiteSpace(txtCodeObjectName.Text))
            {
                MessageBox.Show("Please enter a CodeObject name.", "Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            if (m_ProjectId == Guid.Empty || m_Data == null)
            {
                MessageBox.Show("Project ID or Data connection was not provide.", "Invalid Paramters", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult = DialogResult.Cancel;

                return;
            }

            (bool exists, Guid id) = DataValidator.CodeObjectExists(m_ProjectId, txtCodeObjectName.Text, m_Data);

            if (exists)
            {
                MessageBox.Show(
                    $"A Code Object named '{txtCodeObjectName.Text}' already exists in this project.\nPlease choose a different name.",
                    "Duplicate Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);               
                                
                return;
            }

            NewCodeObject = new CodeObject
            {
                ProjectId = ProjectId,
                Name = txtCodeObjectName.Text.Trim(),
                Type = cmbCodeObjectType.Text.Trim(),
            };

            m_Data.CodeObjects.Add(NewCodeObject);
            JsonStorage.Save(m_Data);

            MessageBox.Show($"Code Object '{NewCodeObject.Name}' created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Creates new codeobject.
        /// </summary>
        /// <value>The new code object.</value>
        public CodeObject NewCodeObject { private set; get; }
    }
}

using CodeTimeTracker.Data;
using CodeTimeTracker.Data.Models;
using System.DirectoryServices.ActiveDirectory;

namespace CodeTimeTracker
{
    public partial class NewProjectWindow : Form
    {
        #region Methods

        #region Constructors

        public NewProjectWindow() 
        {
            InitializeComponent();            

            WireEvents();
        }

        #endregion

        private void WireEvents()
        {
            btnCreate.Click += btnCreate_Click!;
        }

        #region Event Handlers

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

        private TimeTrackerData m_Data = null!;

        public TimeTrackerData Data
        {
            get { return m_Data; }
            set
            {
                if (m_Data == null)
                    m_Data = value;
            }
        }
        
        public Project NewProject { private set; get; }

        #endregion
    }
}

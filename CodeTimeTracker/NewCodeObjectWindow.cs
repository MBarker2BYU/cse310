using CodeTimeTracker.Data;
using CodeTimeTracker.Data.Models;

namespace CodeTimeTracker
{
    public partial class NewCodeObjectWindow : Form
    {

        private Guid m_ProjectId = Guid.Empty;

        public Guid ProjectId
        {
            get { return m_ProjectId; }
            set
            {
                if (m_ProjectId == Guid.Empty)
                    m_ProjectId = value;
            }
        }

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


        public NewCodeObjectWindow()
        {
            InitializeComponent();
            InitializeUI();
        }
        
        private void WireEvents()
        { 
            btnCreate.Click += btnCreate_Click!;
        }

        private void InitializeUI()
        {
            cmbCodeObjectType.Items.AddRange(new[] { "Form", "Class", "UserControl", "Service", "ViewModel", "Component", "Controller", "Model", "View", "" });
            cmbCodeObjectType.SelectedIndex = cmbCodeObjectType.Items.Count - 1;

            WireEvents();
        }

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

        public CodeObject NewCodeObject { private set; get; }
    }
}

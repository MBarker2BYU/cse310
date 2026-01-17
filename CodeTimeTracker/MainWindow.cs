using CodeTimeTracker.Data;          // For JsonStorage
using CodeTimeTracker.Data.Models;
using CodeTimeTracker.Extensions;  // For TimeTrackerData, Project, etc.

namespace CodeTimeTracker
{
    public partial class MainWindow : Form
    {
        private TimeTrackerData m_Data;  // Holds the loaded projects, objects, entries

        private TimeEntry? m_CurrentEntry = null;
        private System.Windows.Forms.Timer m_Timer = new();
        private DateTime m_LastTick = DateTime.Now;

        #region Events

        private void addManualEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using TimeEntryForm form = new(m_Data, cmbProject.SelectedItem as Project);
            if (form.ShowDialog() == DialogResult.OK)
            {
                JsonStorage.Save(m_Data);

                RefreshProjectDropdown();
                RefreshCodeObjectDropdown();
                RefreshEntriesGrid();
            }
        }

        #endregion

        #region Methods

        #region Constructors

        #endregion

        #endregion

        public MainWindow()
        {
            InitializeComponent();
           
            dgvEntries.ContextMenuStrip = contextMenuGrid;

            LoadData();                 // Load JSON on startup
            RefreshProjectDropdown();   // Show projects in combo box
            RefreshEntriesGrid();

            // Timer setup: updates UI every second while running
            m_Timer.Interval = 1000; // 1 second
            m_Timer.Tick += Timer_Tick;
        }

        private void LoadData()
        {
            try
            {
                m_Data = JsonStorage.Load();

                // Optional: Show a message if no data yet (first run)
                if (m_Data.Projects.Count == 0)
                {
                    lblCurrentStatus.Text = "No projects yet. Click 'New Project...' to start.";
                    lblCurrentStatus.ForeColor = Color.DarkOrange;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data file:\n{ex.Message}\n\nStarting with empty data.",
                    "Data Load Issue", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                m_Data = new TimeTrackerData();
            }
        }

        private void RefreshProjectDropdown()
        {
            cmbProject.Items.Clear();

            if (m_Data.Projects.Count == 0)
            {               
                btnNewCodeObject.Enabled = false;
                txtTaskName.Enabled = false;
                btnStart.Enabled = false;
                return;
            }

            cmbProject.LoadProjects(m_Data);
                       
            btnNewCodeObject.Enabled = true;
            txtTaskName.Enabled = true;
            btnStart.Enabled = true;

            lblCurrentStatus.Text = $"Loaded {m_Data.Projects.Count} project(s). Ready to track time.";
        }

        // Temporary: Quick way to test adding a project manually (remove later)
        private void btnNewProject_Click(object sender, EventArgs e)
        {

            var newProjectWindow = new NewProjectWindow() { Data = m_Data };

            if (newProjectWindow.ShowDialog() != DialogResult.OK && !string.IsNullOrWhiteSpace(newProjectWindow.NewProject?.Name))
                return;

            RefreshProjectDropdown();

            if (newProjectWindow.NewProject != null)
                cmbProject.SelectedItem = newProjectWindow.NewProject;

            RefreshEntriesGrid();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // === Resume case: if we have a paused entry ===
            if (m_CurrentEntry != null && m_CurrentEntry.EndTime == null)
            {
                // Already running or paused → just restart timer
                m_Timer.Start();
                m_LastTick = DateTime.Now;
                UpdateStatusLabel();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnPause.Enabled = true;
                return;
            }

            // === New tracking case ===
            if (cmbProject.SelectedItem == null || cmbCodeObject.SelectedItem == null || string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                MessageBox.Show("Select a Project, Code Object, and enter a Task Name before starting.",
                                "Missing Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CodeObject selectedCodeObj = (CodeObject)cmbCodeObject.SelectedItem;

            m_CurrentEntry = new TimeEntry
            {
                CodeObjectId = selectedCodeObj.Id,
                TaskName = txtTaskName.Text.Trim(),
                StartTime = DateTime.Now,
                Notes = ""
            };

            m_Data.TimeEntries.Add(m_CurrentEntry);
            JsonStorage.Save(m_Data);

            m_Timer.Start();
            m_LastTick = DateTime.Now;

            UpdateStatusLabel();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnPause.Enabled = true;
            txtTaskName.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (m_CurrentEntry == null) return;

            m_Timer.Stop();

            m_CurrentEntry.EndTime = DateTime.Now;
            JsonStorage.Save(m_Data);

            m_CurrentEntry = null;

            UpdateStatusLabel();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            txtTaskName.Enabled = true;
            txtTaskName.Clear();

            RefreshEntriesGrid(); // We'll add this method next step
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (m_CurrentEntry == null) return;

            m_Timer.Stop();

            // Keep _currentEntry alive so we can resume
            UpdateStatusLabel("Paused. Click START to resume this task.");

            btnStart.Enabled = true;      // Allow resume
            btnStop.Enabled = true;       // Allow final stop
            btnPause.Enabled = false;     // Can't pause again while paused
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (m_CurrentEntry == null) return;

            // Update status every second
            UpdateStatusLabel();
        }

        private void UpdateStatusLabel(string overrideText = null)
        {
            if (overrideText != null)
            {
                lblCurrentStatus.Text = overrideText;
                lblCurrentStatus.ForeColor = Color.DarkOrange;
                return;
            }

            if (m_CurrentEntry == null)
            {
                lblCurrentStatus.Text = "No timer running...";
                lblCurrentStatus.ForeColor = Color.DarkSlateGray;
                return;
            }

            var duration = m_CurrentEntry.Duration;
            string status = $"Tracking: {duration.Hours:D2}:{duration.Minutes:D2}:{duration.Seconds:D2} | Task: {m_CurrentEntry.TaskName}";
            lblCurrentStatus.Text = status;
            lblCurrentStatus.ForeColor = Color.DarkGreen;
        }

        private void RefreshCodeObjectDropdown()
        {
            cmbCodeObject.LoadCodeObjects(m_Data, cmbProject, btnNewCodeObject);            
        }

        private void cmbProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCodeObjectDropdown();
            txtTaskName.Clear();
            UpdateStatusLabel();

            RefreshEntriesGrid();
        }

        private void btnNewCodeObject_Click(object sender, EventArgs e)
        {

            if (cmbProject.SelectedItem == null || !(cmbProject.SelectedItem is Project selectedProject))
            {
                MessageBox.Show("Select a project first.", "No Project Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var newCodeObjectWindow = new NewCodeObjectWindow() { ProjectId = selectedProject.Id, Data = m_Data };

            if (newCodeObjectWindow.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(newCodeObjectWindow.NewCodeObject?.Name))
            {
                RefreshCodeObjectDropdown();

                cmbCodeObject.SelectedItem = newCodeObjectWindow.NewCodeObject;

                RefreshEntriesGrid();
            }
        }

        private void RefreshEntriesGrid()
        {
            dgvEntries.Rows.Clear();
            dgvEntries.Columns.Clear();

            dgvEntries.Columns.Add("CodeObject", "Code Object");
            dgvEntries.Columns.Add("Type", "Type");
            dgvEntries.Columns.Add("Task", "Task");
            dgvEntries.Columns.Add("Start", "Start Time");
            dgvEntries.Columns.Add("End", "End Time");
            dgvEntries.Columns.Add("Duration", "Duration");

            using (DataGridViewTextBoxColumn idColumn = new() { Name = "EntryId", Visible = false })
            {
                dgvEntries.Columns.Add(idColumn);
            }

            dgvEntries.Columns["Duration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            var visibleTotal = TimeSpan.Zero;

            var visibleEntries = m_Data.TimeEntries
                .Where(en => chkShowDeleted.Checked || !en.IsDeleted)
                .OrderByDescending(e => e.StartTime);

            foreach (var entry in visibleEntries)
            {
                var codeObj = m_Data.CodeObjects.FirstOrDefault(co => co.Id == entry.CodeObjectId);
                if (codeObj == null) continue;

                if (cmbProject.SelectedItem is Project selectedProject && codeObj.ProjectId != selectedProject.Id)
                    continue;

                int rowIndex = dgvEntries.Rows.Add(
                    codeObj.Name ?? "Unknown",
                    codeObj.Type ?? "",
                    entry.TaskName,
                    entry.StartTime.ToString("MM/dd HH:mm"),
                    entry.EndTime?.ToString("MM/dd HH:mm") ?? "Running",
                    entry.DurationFormatted,
                    entry.Id.ToString()
                );

                if (entry.IsDeleted)
                {
                    var row = dgvEntries.Rows[rowIndex];
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.Font = new Font(dgvEntries.Font, FontStyle.Strikeout);
                }

                if (entry.IsDeleted)
                    continue;

                visibleTotal += entry.Duration;
            }

            string prefix = "Total Project Time: ";

            lblTotalTime.Text = $"{prefix}{(visibleTotal.Days * 24) + visibleTotal.Hours:D2} hours and {visibleTotal.Minutes:D2} minutes";

            dgvEntries.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            RefreshEntriesGrid();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvEntries.CurrentRow == null) return;

            string idStr = dgvEntries.CurrentRow.Cells["EntryId"].Value?.ToString();
            if (!Guid.TryParse(idStr, out Guid entryId)) return;

            var entry = m_Data.TimeEntries.FirstOrDefault(en => en.Id == entryId);
            if (entry == null) return;

            if (MessageBox.Show("Soft delete this entry? (It will be hidden but kept in data)",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                entry.IsDeleted = true;
                JsonStorage.Save(m_Data);
                RefreshEntriesGrid();
            }
        }

        private void btnExportFullTxt_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveDialog = new()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = "Export Full Report (TXT)",
                FileName = "CodeTimeTracker_Full_Report.txt",
                DefaultExt = "txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    JsonStorage.ExportToTxt(saveDialog.FileName, m_Data);
                    MessageBox.Show("Full report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportFullCsv_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveDialog = new()
            {
                Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                Title = "Export Full Data (CSV)",
                FileName = "CodeTimeTracker_Full_Data.csv",
                DefaultExt = "csv"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    JsonStorage.ExportToCsv(saveDialog.FileName, m_Data);
                    MessageBox.Show("Full CSV exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExportProject_Click(object sender, EventArgs e)
        {
            if (cmbProject.SelectedItem == null || !(cmbProject.SelectedItem is Project selectedProject))
            {
                MessageBox.Show("Select a project first to export its data.", "No Project Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using SaveFileDialog saveDialog = new()
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                Title = $"Export {selectedProject.Name} Report (TXT)",
                FileName = $"{selectedProject.Name.Replace(" ", "_")}_Report.txt",
                DefaultExt = "txt"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    JsonStorage.ExportToTxt(saveDialog.FileName, m_Data, selectedProject.Id);
                    MessageBox.Show($"Project '{selectedProject.Name}' exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Export failed: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvEntries_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Select the row
                dgvEntries.CurrentCell = dgvEntries.Rows[e.RowIndex].Cells[e.ColumnIndex];

                var row = dgvEntries.Rows[e.RowIndex];

                // Get entry ID from hidden column
                if (!Guid.TryParse(row.Cells["EntryId"].Value?.ToString(), out Guid entryId))
                    return;

                var entry = m_Data.TimeEntries.FirstOrDefault(en => en.Id == entryId);
                if (entry == null) return;

                // Enable/disable based on state
                contextMenuGrid.Items["editEntryToolStripMenuItem"].Enabled = !entry.IsDeleted;
                contextMenuGrid.Items["deleteEntryToolStripMenuItem"].Enabled = !entry.IsDeleted;
                contextMenuGrid.Items["restoreEntryToolStripMenuItem"].Enabled = entry.IsDeleted;

                // Show the menu
                contextMenuGrid.Show(dgvEntries, dgvEntries.PointToClient(Cursor.Position));
            }
        }

        private void editEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvEntries.CurrentRow == null) return;

            if (!Guid.TryParse(dgvEntries.CurrentRow.Cells["EntryId"].Value?.ToString(), out Guid entryId))
                return;

            var entry = m_Data.TimeEntries.FirstOrDefault(en => en.Id == entryId);
            if (entry == null || entry.IsDeleted) return;

            using TimeEntryForm form = new(m_Data, entry);
            if (form.ShowDialog() == DialogResult.OK)
            {
                JsonStorage.Save(m_Data);
                RefreshEntriesGrid();
                UpdateStatusLabel();
            }
        }

        private void deleteEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvEntries.CurrentRow == null) return;

            if (!Guid.TryParse(dgvEntries.CurrentRow.Cells["EntryId"].Value?.ToString(), out Guid entryId))
                return;

            var entry = m_Data.TimeEntries.FirstOrDefault(en => en.Id == entryId);
            if (entry == null || entry.IsDeleted) return;

            if (MessageBox.Show("Soft delete this entry? It will be hidden but kept in data.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                entry.IsDeleted = true;
                JsonStorage.Save(m_Data);
                RefreshEntriesGrid();
            }
        }

        private void restoreEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvEntries.CurrentRow == null) return;

            if (!Guid.TryParse(dgvEntries.CurrentRow.Cells["EntryId"].Value?.ToString(), out Guid entryId))
                return;

            var entry = m_Data.TimeEntries.FirstOrDefault(en => en.Id == entryId);
            if (entry == null || !entry.IsDeleted) return;

            if (MessageBox.Show("Restore this deleted entry?",
                "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                entry.IsDeleted = false;
                JsonStorage.Save(m_Data);
                RefreshEntriesGrid();
            }
        }
        
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            var aboutWindow = new AboutWindow();

            aboutWindow.ShowDialog();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuReset_Click(object sender, EventArgs e)
        {
            var warning1 = MessageBox.Show(
                "This will PERMANENTLY DELETE all projects, code objects, and time entries.\n" +
                "This action CANNOT be undone.\n\n" +
                "Are you absolutely sure you want to continue?",
                "CRITICAL WARNING – Data Loss",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Error,
                MessageBoxDefaultButton.Button2); 

            if (warning1 != DialogResult.Yes)
                return;

            string confirmPhrase = "DELETE ALL";
            string userInput = Microsoft.VisualBasic.Interaction.InputBox(
                $"To confirm deletion, type the phrase exactly:\n\n{confirmPhrase}\n\n" +
                "This is your last chance to cancel.",
                "FINAL CONFIRMATION",
                "");

            if (userInput?.Trim() != confirmPhrase)
            {
                MessageBox.Show("Deletion cancelled – phrase did not match.", "Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {             

                // Clear in-memory data
                m_Data.Projects.Clear();
                m_Data.CodeObjects.Clear();
                m_Data.TimeEntries.Clear();

                JsonStorage.Save(m_Data);

                // Refresh UI completely
                RefreshProjectDropdown();
                RefreshCodeObjectDropdown();
                RefreshEntriesGrid();
                UpdateStatusLabel("All data deleted. Starting fresh.");

                MessageBox.Show("All data has been permanently deleted.\nThe application will now start fresh.",
                    "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting data file:\n{ex.Message}",
                    "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
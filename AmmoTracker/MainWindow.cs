using AmmoTracker.Databases;
using System.Data;

namespace AmmoTracker
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            InitializeAmmoTracker();
        }

        private void InitializeAmmoTracker()
        {
            m_Database = new Database();

            cboType.DataSource = m_Database.CRUD.GetAmmoTypes();
            cboType.DisplayMember = "Name";
            cboType.ValueMember = "TypeID";

            // Configure NumericUpDown controls
            nudRounds.Minimum = 0;          // or 1 if you want minimum 1 round
            nudRounds.Maximum = 1000000;    // reasonable upper limit
            nudRounds.Increment = 10;       // step size
            nudRounds.DecimalPlaces = 0;    // whole numbers only

            nudCost.Minimum = 0;
            nudCost.Maximum = 1000;         // per-round cost limit
            nudCost.Increment = 0.01M;
            nudCost.DecimalPlaces = 2;      // two decimal places for currency

            RefreshGrid();
            ResetToAddMode();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_editingLotId.HasValue)
            {
                btnUpdate_Click(sender, e);
                return;
            }

            if (cboType.SelectedValue == null)
            {
                MessageBox.Show("Please select an ammo type.");
                return;
            }

            int typeId = Convert.ToInt32(cboType.SelectedValue);
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            int rounds = (int)nudRounds.Value;
            decimal cost = nudCost.Value;

            if (rounds <= 0)
            {
                MessageBox.Show("Rounds must be greater than 0.");
                return;
            }

            try
            {
                m_Database.CRUD.AddLot(typeId, date, rounds, cost);
                RefreshGrid();
                MessageBox.Show("Lot added successfully!");
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding lot: {ex.Message}");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!_editingLotId.HasValue)
            {
                MessageBox.Show("No lot selected for update.");
                return;
            }

            int typeId = Convert.ToInt32(cboType.SelectedValue);
            string date = dtpDate.Value.ToString("yyyy-MM-dd");
            int rounds = (int)nudRounds.Value;
            decimal cost = nudCost.Value;

            if (rounds <= 0)
            {
                MessageBox.Show("Rounds must be greater than 0.");
                return;
            }

            try
            {
                m_Database.CRUD.UpdateLot(_editingLotId.Value, typeId, date, rounds, cost);
                RefreshGrid();
                MessageBox.Show("Lot updated successfully!");
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating lot: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select a lot to delete.");
                return;
            }

            int lotId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LotID"].Value);

            if (MessageBox.Show("Delete this lot?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    m_Database.CRUD.DeleteLot(lotId);
                    RefreshGrid();
                    ResetToAddMode();
                    MessageBox.Show("Lot deleted.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting lot: {ex.Message}");
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                ResetToAddMode();
                return;
            }

            var row = dataGridView1.SelectedRows[0];
            _editingLotId = Convert.ToInt32(row.Cells["LotID"].Value);

            // Auto-fill controls
            cboType.SelectedValue = row.Cells["TypeID"]?.Value ?? DBNull.Value;
            dtpDate.Value = DateTime.TryParse(row.Cells["PurchaseDate"].Value?.ToString(), out DateTime parsedDate)
                ? parsedDate
                : DateTime.Today;
            nudRounds.Value = Convert.ToDecimal(row.Cells["Rounds"].Value ?? 0);
            nudCost.Value = Convert.ToDecimal(row.Cells["CostPerRound"].Value ?? 0);

            btnAdd.Text = "Update Lot";
            btnUpdate.Enabled = true;
        }

        private void RefreshGrid()
        {
            string search = txtSearch.Text.Trim();
            DataTable dt = m_Database.CRUD.GetAllLotsWithTypes(search);
            dataGridView1.DataSource = dt;

            var (totalRounds, lotCount) = m_Database.CRUD.GetSummary();
            lblTotal.Text = $"Total Rounds: {totalRounds:N0}";
            lblCount.Text = $"Total Lots: {lotCount}";

            int lowTypes = m_Database.CRUD.GetLowStockTypeCount(500);
            lblAlert.Text = lowTypes > 0 ? $"LOW STOCK ALERT! ({lowTypes} type{(lowTypes == 1 ? "" : "s")})" : "";
            lblAlert.ForeColor = lowTypes > 0 ? System.Drawing.Color.Red : System.Drawing.Color.Black;
        }

        private void ResetToAddMode()
        {
            _editingLotId = null;
            cboType.SelectedIndex = -1;
            dtpDate.Value = DateTime.Today;
            nudRounds.Value = 0;
            nudCost.Value = 0.00M;
            btnAdd.Text = "Add Lot";
            btnUpdate.Enabled = false;
        }


        private Database m_Database;
        private int? _editingLotId = null;
    }
}

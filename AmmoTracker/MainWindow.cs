using AmmoTracker.Databases;
using AmmoTracker.Extensions;
using AmmoTracker.Models;

namespace AmmoTracker;

public partial class MainWindow : Form
{
    public MainWindow()
    {
        InitializeComponent();

        dgvInventory.FixHeaderResizeDrawIssue();
        dgvPurchaseHistory.FixHeaderResizeDrawIssue();

        m_DatabaseHelper = new DatabaseHelper();

        ConfigureInventoryGrid();
        ConfigurePurchaseHistoryGrid();

        UpdateUI();
    }

    private DataGridViewTextBoxColumn AddColumnFormat(string propertyName, bool visible = true)
    {
        return new DataGridViewTextBoxColumn
        {
            Name = propertyName,
            DataPropertyName = propertyName,
            Visible = visible
        };
    }

    private void ConfigureInventoryGrid()
    {
        dgvInventory.AutoGenerateColumns = false;
        dgvInventory.Columns.Clear();

        dgvInventory.AddColumnFormat("TypeID", false);
        dgvInventory.AddColumnFormat("ManufacturerName", "Manufacturer");
        dgvInventory.AddColumnFormat("CaliberName", "Caliber");
        dgvInventory.AddColumnFormat("GrainValue", "Grain");
        dgvInventory.AddColumnFormat("CurrentRounds", "Rounds", "N0");
        dgvInventory.AddColumnFormat("MinimumThreshold", "Min Threshold", "N0");
        dgvInventory.AddColumnFormat("Status", "Status");
        dgvInventory.AddColumnFormat("TotalValue", "Total Value", "C2");

    }

    private void ConfigurePurchaseHistoryGrid()
    {
        dgvPurchaseHistory.AddColumnFormat("PurchaseID", false);
        dgvPurchaseHistory.AddColumnFormat("TypeID", false);
        dgvPurchaseHistory.AddColumnFormat("PurchaseDate", "Purchase Date");
        dgvPurchaseHistory.AddColumnFormat("RoundsAdded", "Rounds Added");
        dgvPurchaseHistory.AddColumnFormat("RoundsPerContainer", "Per Container");
        dgvPurchaseHistory.AddColumnFormat("Containers", "");
        dgvPurchaseHistory.AddColumnFormat("LotNumber", "Lot");
        dgvPurchaseHistory.AddColumnFormat("CostPerRound", "Cost Per Round", "C2");
        dgvPurchaseHistory.AddColumnFormat("TotalCost", "Total Cost", "C2");

    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        var purchaseWindow = new PurchaseWindow(m_DatabaseHelper);

        if (purchaseWindow.ShowDialog(this) != DialogResult.OK)
            return;

        UpdateUI();

    }

    private void UpdateUI(bool gridOnly = false)
    {
        if(!gridOnly)
            LoadComboBoxes();

        dgvInventory.DataSource = m_InventoryFilter != null ? m_DatabaseHelper.CRUD.GetInventoryByFilter(m_InventoryFilter) : m_DatabaseHelper.CRUD.GetInventoryByType(); 

        var (total, low) = m_DatabaseHelper.CRUD.GetSummaryStats();
        lblTotalRounds.Text = total.ToString();
        lblQuantityStock.Text = low.ToString();
    }

    private readonly DatabaseHelper m_DatabaseHelper;

    private void btnRefresh_Click(object sender, EventArgs e)
    {
        UpdateUI();
    }

    private void dgvInventory_SelectionChanged(object sender, EventArgs e)
    {

        if (!GetInventorySelectedRowId(out var id))
        {
            dgvPurchaseHistory.DataSource = null;

            return;
        }

        dgvPurchaseHistory.DataSource = m_DatabaseHelper.CRUD.GetPurchasesByTypeId(id);
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (!GetPurchaseId(out var purchaseId))
            return;

        var purchaseWindow = new PurchaseWindow(m_DatabaseHelper, purchaseId);

        if (purchaseWindow.ShowDialog(this) != DialogResult.OK)
            return;

        UpdateUI();
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show(@"Do you want delete all the purchases linked?", @"AmmoTracker [Delete Purchase]", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            return;

        if (!GetPurchaseId(out var id))
            return;

        m_DatabaseHelper.CRUD.DeletePurchaseById(id);

        UpdateUI();

    }

    private bool GetInventorySelectedRowId(out long id)
    {
        id = -1;

        if (dgvInventory.SelectedRows.Count == 0)
        {
            return false;
        }

        // Get TypeID from the selected row
        id = Convert.ToInt64(dgvInventory.SelectedRows[0].Cells["TypeID"].Value);

        return true;
    }

    private bool GetPurchaseId(out int id)
    {
        id = -1;

        if (dgvPurchaseHistory.SelectedRows.Count == 0)
        {
            MessageBox.Show(@"Please select a row first.", @"No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Get PurchaseID from the selected row
        id = Convert.ToInt32(dgvPurchaseHistory.SelectedRows[0].Cells["PurchaseID"].Value);

        return true;
    }

    private void LoadManufacturers()
    {
        // Manufacturers
        var manufacturers = m_DatabaseHelper.CRUD.GetManufacturers();

        manufacturers.Insert(0, new ComboItem { Id = -1, Name = "[Select Manufacturer...]" });

        cboManufacturer.DataSource = manufacturers;
        cboManufacturer.DisplayMember = "Name";
        cboManufacturer.ValueMember = "Id";
    }

    private void LoadCalibers()
    {
        // Calibers (same pattern)
        var calibers = m_DatabaseHelper.CRUD.GetCalibers();

        calibers.Insert(0, new ComboItem { Id = -1, Name = "[Select Caliber...]" });

        cboCaliber.DataSource = calibers;
        cboCaliber.DisplayMember = "Name";
        cboCaliber.ValueMember = "Id";
    }

    private void LoadGrains()
    {
        // Grains (same pattern)
        var grains = m_DatabaseHelper.CRUD.GetGrains();

        grains.Insert(0, new ComboItem { Id = -1, Name = "[Select Grain...]" });

        cboGrain.DataSource = grains;
        cboGrain.DisplayMember = "Name";
        cboGrain.ValueMember = "Id";
    }

    private void LoadLotNumbersCombo()
    {
        var lotNumbers = m_DatabaseHelper.CRUD.GetDistinctLotNumbers();

        lotNumbers.Insert(0, "[Select Lot#...]");

        cboLotNumber.Items.Clear();
        cboLotNumber.Items.AddRange([.. lotNumbers]);
        cboLotNumber.SelectedIndex = 0;
    }

    private void LoadComboBoxes()
    {
        LoadManufacturers();
        LoadCalibers();
        LoadGrains();
        LoadLotNumbersCombo();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
        LoadComboBoxes();

        chkUseDates.Checked = false;
        dtpStartDate.Value = DateTime.Now;
        dtpEndDate.Value = DateTime.Now;

        m_InventoryFilter = null;

        UpdateUI();
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
        m_InventoryFilter = new InventoryFilter();

        if (cboManufacturer.SelectedValue is long manufacturerId and > 0)
            m_InventoryFilter.ManufacturerId = manufacturerId;

        if (cboCaliber.SelectedValue is long caliberId and > 0)
            m_InventoryFilter.CaliberId = caliberId;

        if (cboGrain.SelectedValue is long grainId and > 0)
            m_InventoryFilter.GrainId = grainId;

        m_InventoryFilter.StartDate = dtpStartDate.Enabled ? dtpStartDate.Value : null;
        m_InventoryFilter.EndDate = dtpEndDate.Enabled ? dtpEndDate.Value : null;

        var lotText = cboLotNumber.Text?.Trim();
        if (string.IsNullOrWhiteSpace(lotText) || lotText == "[Select Lot#...]")
        {
            m_InventoryFilter.LotNumber = null;
        }
        else
        {
            m_InventoryFilter.LotNumber = lotText;  // pass as-is for LIKE '%lotText%'
        }
        
        UpdateUI(true);

    }

    private void chkUseDates_CheckedChanged(object sender, EventArgs e)
    {
        dtpStartDate.Enabled = dtpEndDate.Enabled = chkUseDates.Checked;
    }

    private void dgvInventory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var typeId = Convert.ToInt64(dgvInventory.Rows[e.RowIndex].Cells["TypeID"].Value);
        var minimumThreshold = Convert.ToInt64(dgvInventory.Rows[e.RowIndex].Cells["MinimumThreshold"].Value);

        using var form = new MinimumThreshold(m_DatabaseHelper, typeId, minimumThreshold);

        if (form.ShowDialog() == DialogResult.OK)
        {
            UpdateUI(true);
        }
    }

    private InventoryFilter? m_InventoryFilter;

}


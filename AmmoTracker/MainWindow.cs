using AmmoTracker.Databases;
using AmmoTracker.Extensions;
using System.Reflection;
using System.Windows.Forms;

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
            Name = propertyName, DataPropertyName = propertyName, Visible = visible
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

    private void UpdateUI()
    {
        dgvInventory.DataSource = m_DatabaseHelper.CRUD.GetInventoryByType();

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

        if(!GetInventorySelectedRowId(out var id))
            return;

        dgvPurchaseHistory.DataSource = m_DatabaseHelper.CRUD.GetPurchasesByTypeId(id);

        //var hasSelection = dgvInventory.SelectedRows.Count > 0;

        //btnEdit.Enabled = hasSelection;
        //btnDelete.Enabled = hasSelection;
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
        if(MessageBox.Show(@"Do you want delete all the purchases linked?", @"AmmoTracker [Delete Purchase]", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            return;

        if(!GetPurchaseId(out var id))
            return;

        m_DatabaseHelper.CRUD.DeletePurchaseById(id);

        UpdateUI();

    }

    private bool GetInventorySelectedRowId(out int id)
    {
        id = -1;

        if (dgvInventory.SelectedRows.Count == 0)
        {
            //MessageBox.Show(@"Please select a row first.", @"No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        // Get TypeID from the selected row
        id = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["TypeID"].Value);

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

    
}


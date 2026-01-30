using AmmoTracker.Databases;
using AmmoTracker.Models;

namespace AmmoTracker
{
    public partial class PurchaseWindow : Form
    {

        #region Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            var manufacturerId = Convert.ToInt32(cboManufacturer.SelectedValue!);
            var caliberId = Convert.ToInt32(cboCaliber.SelectedValue!);
            var grainId = Convert.ToInt32(cboGrain.SelectedValue!);

            var typeId = m_DatabaseHelper.CRUD.GetOrCreateAmmoType(manufacturerId, caliberId, grainId);

            if (!m_IsUpdate)
            {

                var roundsAdded = (int)nudQuantity.Value; // or calculate from containers × per container
                var costPerRound = nudCostPerRound.Value;

                m_DatabaseHelper.CRUD.AddPurchase(
                    typeId,
                    dtpPurchaseDate.Value,
                    roundsAdded,
                    (int)nudRoundsPerContainer.Value,
                    (int)nudContainers.Value,
                    txtLotNumber.Text.Trim(),
                    costPerRound);
            }
            else
            {
                // Update existing purchase
                if (m_PurchaseItem != null)
                {
                    m_PurchaseItem.TypeID = typeId;
                    m_PurchaseItem.PurchaseDate = dtpPurchaseDate.Value;
                    m_PurchaseItem.RoundsAdded = (int)nudQuantity.Value;
                    m_PurchaseItem.RoundsPerContainer = (int)nudRoundsPerContainer.Value;
                    m_PurchaseItem.Containers = (int)nudContainers.Value;
                    m_PurchaseItem.LotNumber = txtLotNumber.Text.Trim();
                    m_PurchaseItem.CostPerRound = nudCostPerRound.Value;
                    m_DatabaseHelper.CRUD.UpdatePurchase(m_PurchaseItem);
                }
            }

            DialogResult = DialogResult.OK;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        #region Constructors

        public PurchaseWindow()
        {
            InitializeComponent();
        }

        public PurchaseWindow(DatabaseHelper databaseHelper) : this()
        {
            m_DatabaseHelper = databaseHelper;

            InitComboBoxes();
        }

        public PurchaseWindow(DatabaseHelper databaseHelper, int purchaseId) : this(databaseHelper)
        {
            m_IsUpdate = true;

            LoadPurchaseData(purchaseId);
        }

        private void LoadPurchaseData(int purchaseId)
        {
            btnAdd.Text = @"Update";

            m_PurchaseItem = m_DatabaseHelper.CRUD.GetPurchaseById(purchaseId);

            if (m_PurchaseItem != null)
            {
                var ammoType = m_DatabaseHelper.CRUD.GetAmmoTypeById(m_PurchaseItem.TypeID);

                if (ammoType != null)
                {
                    cboManufacturer.SelectedValue = ammoType.ManufacturerID;
                    cboCaliber.SelectedValue = ammoType.CaliberID;
                    cboGrain.SelectedValue = ammoType.GrainID;
                }
            
                dtpPurchaseDate.Value = m_PurchaseItem.PurchaseDate;
                nudQuantity.Value = m_PurchaseItem.RoundsAdded;
                nudRoundsPerContainer.Value = m_PurchaseItem.RoundsPerContainer;
                nudContainers.Value = m_PurchaseItem.Containers;
                txtLotNumber.Text = m_PurchaseItem.LotNumber ?? string.Empty;
                nudCostPerRound.Value = m_PurchaseItem.CostPerRound;
            }
            else
            {
                MessageBox.Show(@"Purchase not found.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

        }

        #endregion

        private bool ValidateInputs()
        {
            // Manufacturer must be selected
            if (cboManufacturer.SelectedValue == null || Convert.ToInt32(cboManufacturer.SelectedValue) <= 0)
            {
                MessageBox.Show(@"Please select a Manufacturer.", @"Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboManufacturer.Focus();
                return false;
            }

            // Caliber must be selected
            if (cboCaliber.SelectedValue == null || Convert.ToInt32(cboCaliber.SelectedValue) <= 0)
            {
                MessageBox.Show(@"Please select a Caliber.", @"Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCaliber.Focus();
                return false;
            }

            // Grain must be selected
            if (cboGrain.SelectedValue == null || Convert.ToInt32(cboGrain.SelectedValue) <= 0)
            {
                MessageBox.Show(@"Please select a Grain weight.", @"Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboGrain.Focus();
                return false;
            }

            // Purchase Date should be reasonable (not future or ancient)
            if (dtpPurchaseDate.Value > DateTime.Today.AddDays(1))
            {
                MessageBox.Show(@"Purchase date cannot be in the future.", @"Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpPurchaseDate.Focus();
                return false;
            }
            if (dtpPurchaseDate.Value < DateTime.Today.AddYears(-20))
            {
                MessageBox.Show(@"Purchase date seems too old. Please check.", @"Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpPurchaseDate.Focus();
                return false;
            }

            // Rounds Added must be positive
            if (nudQuantity.Value <= 0)
            {
                MessageBox.Show(@"You must add at least 1 round.", @"Invalid Quantity", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudQuantity.Focus();
                return false;
            }

            // Rounds per container must be positive
            if (nudRoundsPerContainer.Value <= 0)
            {
                MessageBox.Show(@"Rounds per container must be at least 1.", @"Invalid Container", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudRoundsPerContainer.Focus();
                return false;
            }

            // Containers must be at least 1
            if (nudContainers.Value < 1)
            {
                MessageBox.Show(@"Number of containers must be at least 1.", @"Invalid Container Count", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudContainers.Focus();
                return false;
            }

            // Cost per round must be non-negative
            if (nudCostPerRound.Value < 0)
            {
                MessageBox.Show(@"Cost per round cannot be negative.", @"Invalid Cost", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nudCostPerRound.Focus();
                return false;
            }

            // Optional: LotNumber can be empty, but trim whitespace if entered
            if (!string.IsNullOrWhiteSpace(txtLotNumber.Text))
            {
                txtLotNumber.Text = txtLotNumber.Text.Trim();
            }

            return true;
        }

        #endregion

        #region Properties & Fields

        private readonly DatabaseHelper m_DatabaseHelper;

        #endregion

        private void btnAddManufacturer_Click(object sender, EventArgs e)
        {
            var inputForm = new InputForm(m_DatabaseHelper, "Manufacturers", "ManufacturerName", "Manufacturer");

            if (inputForm.ShowDialog() != DialogResult.OK)
                return;

            LoadManufacturers();

        }

        private void btnAddCaliber_Click(object sender, EventArgs e)
        {
            var inputForm = new InputForm(m_DatabaseHelper, "Calibers", "CaliberName", "Caliber");

            if (inputForm.ShowDialog() != DialogResult.OK)
                return;

            LoadCalibers();
        }

        private void btnAddGrain_Click(object sender, EventArgs e)
        {
            var inputForm = new InputForm(m_DatabaseHelper, "Grains", "GrainValue", "Grain");

            if (inputForm.ShowDialog() != DialogResult.OK)
                return;

            LoadGrains();
        }

        private void LoadManufacturers()
        {
            // Manufacturers
            var manufacturers = m_DatabaseHelper.CRUD.GetManufacturers();
            manufacturers.Insert(0, new ComboItem { Id = -1, Name = "[Add New Manufacturer...]" });

            cboManufacturer.DataSource = manufacturers;
            cboManufacturer.DisplayMember = "Name";
            cboManufacturer.ValueMember = "Id";
        }

        private void LoadCalibers()
        {
            // Calibers (same pattern)
            var calibers = m_DatabaseHelper.CRUD.GetCalibers();
            calibers.Insert(0, new ComboItem { Id = -1, Name = "[Add New Caliber...]" });

            cboCaliber.DataSource = calibers;
            cboCaliber.DisplayMember = "Name";
            cboCaliber.ValueMember = "Id";
        }

        private void LoadGrains()
        {
            // Grains (same pattern)
            var grains = m_DatabaseHelper.CRUD.GetGrains();
            grains.Insert(0, new ComboItem { Id = -1, Name = "[Add New Grain...]" });

            cboGrain.DataSource = grains;
            cboGrain.DisplayMember = "Name";
            cboGrain.ValueMember = "Id";
        }

        private void InitComboBoxes()
        {
            LoadManufacturers();
            LoadCalibers();
            LoadGrains();
        }

        private readonly bool m_IsUpdate = false;
        private PurchaseItem? m_PurchaseItem;
    }
}

namespace AmmoTracker
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            pnlControls = new Panel();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            btnAdd = new Button();
            dgvInventory = new DataGridView();
            pnlDataGrid = new Panel();
            dgvPurchaseHistory = new DataGridView();
            lblPurchaseHistory = new Label();
            lblInventory = new Label();
            panel1 = new Panel();
            grpSearch = new GroupBox();
            chkUseDates = new CheckBox();
            lblLotNumber = new Label();
            lblEndDate = new Label();
            lblStartDate = new Label();
            lblWeight = new Label();
            lblCaliber = new Label();
            lblManufacturer = new Label();
            cboManufacturer = new ComboBox();
            cboCaliber = new ComboBox();
            cboGrain = new ComboBox();
            cboLotNumber = new ComboBox();
            dtpStartDate = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            btnApply = new Button();
            btnClear = new Button();
            pnlLabels = new Panel();
            lblLowQuantityLabel = new Label();
            lblQuantityStock = new Label();
            lblTotalRoundsLable = new Label();
            lblTotalRounds = new Label();
            pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            pnlDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseHistory).BeginInit();
            panel1.SuspendLayout();
            grpSearch.SuspendLayout();
            pnlLabels.SuspendLayout();
            SuspendLayout();
            // 
            // pnlControls
            // 
            pnlControls.Controls.Add(btnEdit);
            pnlControls.Controls.Add(btnDelete);
            pnlControls.Controls.Add(btnRefresh);
            pnlControls.Controls.Add(btnAdd);
            pnlControls.Dock = DockStyle.Bottom;
            pnlControls.Location = new Point(0, 939);
            pnlControls.Name = "pnlControls";
            pnlControls.Size = new Size(1904, 54);
            pnlControls.TabIndex = 0;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEdit.Location = new Point(1686, 10);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 32);
            btnEdit.TabIndex = 3;
            btnEdit.Text = "Edit";
            btnEdit.UseVisualStyleBackColor = true;
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDelete.Location = new Point(1792, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 32);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefresh.Location = new Point(1474, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(100, 32);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(1580, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 32);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // dgvInventory
            // 
            dgvInventory.AllowUserToAddRows = false;
            dgvInventory.AllowUserToDeleteRows = false;
            dgvInventory.AllowUserToResizeRows = false;
            dgvInventory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.Transparent;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvInventory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvInventory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventory.Dock = DockStyle.Top;
            dgvInventory.EnableHeadersVisualStyles = false;
            dgvInventory.Location = new Point(12, 212);
            dgvInventory.MultiSelect = false;
            dgvInventory.Name = "dgvInventory";
            dgvInventory.ReadOnly = true;
            dgvInventory.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.Size = new Size(1880, 403);
            dgvInventory.TabIndex = 1;
            dgvInventory.CellDoubleClick += dgvInventory_CellDoubleClick;
            dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;
            // 
            // pnlDataGrid
            // 
            pnlDataGrid.Controls.Add(dgvPurchaseHistory);
            pnlDataGrid.Controls.Add(lblPurchaseHistory);
            pnlDataGrid.Controls.Add(dgvInventory);
            pnlDataGrid.Controls.Add(lblInventory);
            pnlDataGrid.Controls.Add(panel1);
            pnlDataGrid.Dock = DockStyle.Fill;
            pnlDataGrid.Location = new Point(0, 0);
            pnlDataGrid.Name = "pnlDataGrid";
            pnlDataGrid.Padding = new Padding(12, 12, 12, 0);
            pnlDataGrid.Size = new Size(1904, 939);
            pnlDataGrid.TabIndex = 2;
            // 
            // dgvPurchaseHistory
            // 
            dgvPurchaseHistory.AllowUserToAddRows = false;
            dgvPurchaseHistory.AllowUserToDeleteRows = false;
            dgvPurchaseHistory.AllowUserToResizeRows = false;
            dgvPurchaseHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvPurchaseHistory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvPurchaseHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPurchaseHistory.Dock = DockStyle.Fill;
            dgvPurchaseHistory.EnableHeadersVisualStyles = false;
            dgvPurchaseHistory.Location = new Point(12, 646);
            dgvPurchaseHistory.MultiSelect = false;
            dgvPurchaseHistory.Name = "dgvPurchaseHistory";
            dgvPurchaseHistory.ReadOnly = true;
            dgvPurchaseHistory.RowHeadersVisible = false;
            dgvPurchaseHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPurchaseHistory.Size = new Size(1880, 293);
            dgvPurchaseHistory.TabIndex = 2;
            // 
            // lblPurchaseHistory
            // 
            lblPurchaseHistory.AutoSize = true;
            lblPurchaseHistory.Dock = DockStyle.Top;
            lblPurchaseHistory.Location = new Point(12, 615);
            lblPurchaseHistory.Name = "lblPurchaseHistory";
            lblPurchaseHistory.Padding = new Padding(0, 5, 0, 5);
            lblPurchaseHistory.Size = new Size(127, 31);
            lblPurchaseHistory.TabIndex = 4;
            lblPurchaseHistory.Text = "Purchase History";
            // 
            // lblInventory
            // 
            lblInventory.AutoSize = true;
            lblInventory.Dock = DockStyle.Top;
            lblInventory.Location = new Point(12, 186);
            lblInventory.Name = "lblInventory";
            lblInventory.Padding = new Padding(0, 0, 0, 5);
            lblInventory.Size = new Size(76, 26);
            lblInventory.TabIndex = 3;
            lblInventory.Text = "Inventory";
            // 
            // panel1
            // 
            panel1.Controls.Add(grpSearch);
            panel1.Controls.Add(btnApply);
            panel1.Controls.Add(btnClear);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(1880, 174);
            panel1.TabIndex = 5;
            // 
            // grpSearch
            // 
            grpSearch.Controls.Add(chkUseDates);
            grpSearch.Controls.Add(lblLotNumber);
            grpSearch.Controls.Add(lblEndDate);
            grpSearch.Controls.Add(lblStartDate);
            grpSearch.Controls.Add(lblWeight);
            grpSearch.Controls.Add(lblCaliber);
            grpSearch.Controls.Add(lblManufacturer);
            grpSearch.Controls.Add(cboManufacturer);
            grpSearch.Controls.Add(cboCaliber);
            grpSearch.Controls.Add(cboGrain);
            grpSearch.Controls.Add(cboLotNumber);
            grpSearch.Controls.Add(dtpStartDate);
            grpSearch.Controls.Add(dtpEndDate);
            grpSearch.Location = new Point(3, 3);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(1870, 115);
            grpSearch.TabIndex = 8;
            grpSearch.TabStop = false;
            grpSearch.Text = "Search Criteria";
            // 
            // chkUseDates
            // 
            chkUseDates.AutoSize = true;
            chkUseDates.Location = new Point(818, 65);
            chkUseDates.Name = "chkUseDates";
            chkUseDates.Size = new Size(98, 25);
            chkUseDates.TabIndex = 12;
            chkUseDates.Text = "Use Dates";
            chkUseDates.UseVisualStyleBackColor = true;
            chkUseDates.CheckedChanged += chkUseDates_CheckedChanged;
            // 
            // lblLotNumber
            // 
            lblLotNumber.AutoSize = true;
            lblLotNumber.Location = new Point(1660, 69);
            lblLotNumber.Name = "lblLotNumber";
            lblLotNumber.Size = new Size(48, 21);
            lblLotNumber.TabIndex = 11;
            lblLotNumber.Text = "Lot #:";
            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(1322, 69);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(75, 21);
            lblEndDate.TabIndex = 10;
            lblEndDate.Text = "End Date:";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(956, 66);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(81, 21);
            lblStartDate.TabIndex = 9;
            lblStartDate.Text = "Start Date:";
            // 
            // lblWeight
            // 
            lblWeight.AutoSize = true;
            lblWeight.Location = new Point(1646, 31);
            lblWeight.Name = "lblWeight";
            lblWeight.Size = new Size(62, 21);
            lblWeight.TabIndex = 8;
            lblWeight.Text = "Weight:";
            // 
            // lblCaliber
            // 
            lblCaliber.AutoSize = true;
            lblCaliber.Location = new Point(1335, 31);
            lblCaliber.Name = "lblCaliber";
            lblCaliber.Size = new Size(62, 21);
            lblCaliber.TabIndex = 7;
            lblCaliber.Text = "Caliber:";
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(813, 31);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(107, 21);
            lblManufacturer.TabIndex = 6;
            lblManufacturer.Text = "Manufacturer:";
            // 
            // cboManufacturer
            // 
            cboManufacturer.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboManufacturer.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboManufacturer.FormattingEnabled = true;
            cboManufacturer.Location = new Point(926, 28);
            cboManufacturer.Name = "cboManufacturer";
            cboManufacturer.Size = new Size(287, 29);
            cboManufacturer.TabIndex = 0;
            // 
            // cboCaliber
            // 
            cboCaliber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboCaliber.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboCaliber.FormattingEnabled = true;
            cboCaliber.Location = new Point(1403, 28);
            cboCaliber.Name = "cboCaliber";
            cboCaliber.Size = new Size(170, 29);
            cboCaliber.TabIndex = 1;
            // 
            // cboGrain
            // 
            cboGrain.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboGrain.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboGrain.FormattingEnabled = true;
            cboGrain.Location = new Point(1714, 28);
            cboGrain.Name = "cboGrain";
            cboGrain.Size = new Size(150, 29);
            cboGrain.TabIndex = 2;
            // 
            // cboLotNumber
            // 
            cboLotNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboLotNumber.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboLotNumber.FormattingEnabled = true;
            cboLotNumber.Location = new Point(1714, 66);
            cboLotNumber.Name = "cboLotNumber";
            cboLotNumber.Size = new Size(150, 29);
            cboLotNumber.TabIndex = 5;
            // 
            // dtpStartDate
            // 
            dtpStartDate.Enabled = false;
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(1043, 63);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(170, 29);
            dtpStartDate.TabIndex = 3;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Enabled = false;
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(1403, 63);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(170, 29);
            dtpEndDate.TabIndex = 4;
            // 
            // btnApply
            // 
            btnApply.Location = new Point(1675, 124);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(93, 37);
            btnApply.TabIndex = 7;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(1774, 124);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(93, 37);
            btnClear.TabIndex = 6;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // pnlLabels
            // 
            pnlLabels.Controls.Add(lblLowQuantityLabel);
            pnlLabels.Controls.Add(lblQuantityStock);
            pnlLabels.Controls.Add(lblTotalRoundsLable);
            pnlLabels.Controls.Add(lblTotalRounds);
            pnlLabels.Dock = DockStyle.Bottom;
            pnlLabels.Location = new Point(0, 993);
            pnlLabels.Name = "pnlLabels";
            pnlLabels.Size = new Size(1904, 48);
            pnlLabels.TabIndex = 2;
            // 
            // lblLowQuantityLabel
            // 
            lblLowQuantityLabel.AutoSize = true;
            lblLowQuantityLabel.Location = new Point(12, 12);
            lblLowQuantityLabel.Name = "lblLowQuantityLabel";
            lblLowQuantityLabel.Size = new Size(143, 21);
            lblLowQuantityLabel.TabIndex = 3;
            lblLowQuantityLabel.Text = "Low Quantity Alert:";
            // 
            // lblQuantityStock
            // 
            lblQuantityStock.AutoSize = true;
            lblQuantityStock.Location = new Point(161, 12);
            lblQuantityStock.Name = "lblQuantityStock";
            lblQuantityStock.Size = new Size(61, 21);
            lblQuantityStock.TabIndex = 2;
            lblQuantityStock.Text = "{ Alert }";
            // 
            // lblTotalRoundsLable
            // 
            lblTotalRoundsLable.AutoSize = true;
            lblTotalRoundsLable.Location = new Point(1684, 12);
            lblTotalRoundsLable.Name = "lblTotalRoundsLable";
            lblTotalRoundsLable.Size = new Size(102, 21);
            lblTotalRoundsLable.TabIndex = 1;
            lblTotalRoundsLable.Text = "Total Rounds:";
            // 
            // lblTotalRounds
            // 
            lblTotalRounds.AutoSize = true;
            lblTotalRounds.Location = new Point(1801, 12);
            lblTotalRounds.Name = "lblTotalRounds";
            lblTotalRounds.Size = new Size(91, 21);
            lblTotalRounds.TabIndex = 0;
            lblTotalRounds.Text = "000000000";
            lblTotalRounds.TextAlign = ContentAlignment.MiddleRight;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1904, 1041);
            Controls.Add(pnlDataGrid);
            Controls.Add(pnlControls);
            Controls.Add(pnlLabels);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AmmoTracker";
            pnlControls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvInventory).EndInit();
            pnlDataGrid.ResumeLayout(false);
            pnlDataGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseHistory).EndInit();
            panel1.ResumeLayout(false);
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            pnlLabels.ResumeLayout(false);
            pnlLabels.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlControls;
        private DataGridView dgvInventory;
        private Panel pnlDataGrid;
        private Button btnAdd;
        private Panel pnlLabels;
        private Label lblQuantityStock;
        private Label lblTotalRoundsLable;
        private Label lblTotalRounds;
        private Label lblLowQuantityLabel;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Label lblPurchaseHistory;
        private DataGridView dgvPurchaseHistory;
        private Label lblInventory;
        private Panel panel1;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpStartDate;
        private ComboBox cboGrain;
        private ComboBox cboCaliber;
        private ComboBox cboManufacturer;
        private GroupBox grpSearch;
        private Label lblLotNumber;
        private Label lblEndDate;
        private Label lblStartDate;
        private Label lblWeight;
        private Label lblCaliber;
        private Label lblManufacturer;
        private ComboBox cboLotNumber;
        private Button btnApply;
        private Button btnClear;
        private CheckBox chkUseDates;
    }
}

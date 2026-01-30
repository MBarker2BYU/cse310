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
            pnlLabels = new Panel();
            lblLowQuantityLabel = new Label();
            lblQuantityStock = new Label();
            lblTotalRoundsLable = new Label();
            lblTotalRounds = new Label();
            pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvInventory).BeginInit();
            pnlDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPurchaseHistory).BeginInit();
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
            dgvInventory.Location = new Point(12, 38);
            dgvInventory.MultiSelect = false;
            dgvInventory.Name = "dgvInventory";
            dgvInventory.ReadOnly = true;
            dgvInventory.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvInventory.RowHeadersVisible = false;
            dgvInventory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInventory.Size = new Size(1880, 501);
            dgvInventory.TabIndex = 1;
            dgvInventory.SelectionChanged += dgvInventory_SelectionChanged;
            // 
            // pnlDataGrid
            // 
            pnlDataGrid.Controls.Add(dgvPurchaseHistory);
            pnlDataGrid.Controls.Add(lblPurchaseHistory);
            pnlDataGrid.Controls.Add(dgvInventory);
            pnlDataGrid.Controls.Add(lblInventory);
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
            dgvPurchaseHistory.Location = new Point(12, 570);
            dgvPurchaseHistory.MultiSelect = false;
            dgvPurchaseHistory.Name = "dgvPurchaseHistory";
            dgvPurchaseHistory.ReadOnly = true;
            dgvPurchaseHistory.RowHeadersVisible = false;
            dgvPurchaseHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPurchaseHistory.Size = new Size(1880, 369);
            dgvPurchaseHistory.TabIndex = 2;
            // 
            // lblPurchaseHistory
            // 
            lblPurchaseHistory.AutoSize = true;
            lblPurchaseHistory.Dock = DockStyle.Top;
            lblPurchaseHistory.Location = new Point(12, 539);
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
            lblInventory.Location = new Point(12, 12);
            lblInventory.Name = "lblInventory";
            lblInventory.Padding = new Padding(0, 0, 0, 5);
            lblInventory.Size = new Size(76, 26);
            lblInventory.TabIndex = 3;
            lblInventory.Text = "Inventory";
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
    }
}

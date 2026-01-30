namespace AmmoTracker
{
    partial class PurchaseWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            grpPurchaseInfo = new GroupBox();
            lblLotNumber = new Label();
            lblContainers = new Label();
            txtLotNumber = new TextBox();
            nudContainers = new NumericUpDown();
            lblRoundsPerContainer = new Label();
            nudRoundsPerContainer = new NumericUpDown();
            lblTotalCost = new Label();
            label1 = new Label();
            lblPricePerRound = new Label();
            lblQuantity = new Label();
            lblPurchaseDate = new Label();
            nudCostPerRound = new NumericUpDown();
            nudQuantity = new NumericUpDown();
            dtpPurchaseDate = new DateTimePicker();
            lblGrain = new Label();
            lblCaliber = new Label();
            lblManufacturer = new Label();
            btnAddGrain = new Button();
            btnAddCaliber = new Button();
            btnAddManufacturer = new Button();
            cboGrain = new ComboBox();
            cboCaliber = new ComboBox();
            cboManufacturer = new ComboBox();
            btnCancel = new Button();
            btnAdd = new Button();
            grpPurchaseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudContainers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRoundsPerContainer).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCostPerRound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).BeginInit();
            SuspendLayout();
            // 
            // grpPurchaseInfo
            // 
            grpPurchaseInfo.Controls.Add(lblLotNumber);
            grpPurchaseInfo.Controls.Add(lblContainers);
            grpPurchaseInfo.Controls.Add(txtLotNumber);
            grpPurchaseInfo.Controls.Add(nudContainers);
            grpPurchaseInfo.Controls.Add(lblRoundsPerContainer);
            grpPurchaseInfo.Controls.Add(nudRoundsPerContainer);
            grpPurchaseInfo.Controls.Add(lblTotalCost);
            grpPurchaseInfo.Controls.Add(label1);
            grpPurchaseInfo.Controls.Add(lblPricePerRound);
            grpPurchaseInfo.Controls.Add(lblQuantity);
            grpPurchaseInfo.Controls.Add(lblPurchaseDate);
            grpPurchaseInfo.Controls.Add(nudCostPerRound);
            grpPurchaseInfo.Controls.Add(nudQuantity);
            grpPurchaseInfo.Controls.Add(dtpPurchaseDate);
            grpPurchaseInfo.Controls.Add(lblGrain);
            grpPurchaseInfo.Controls.Add(lblCaliber);
            grpPurchaseInfo.Controls.Add(lblManufacturer);
            grpPurchaseInfo.Controls.Add(btnAddGrain);
            grpPurchaseInfo.Controls.Add(btnAddCaliber);
            grpPurchaseInfo.Controls.Add(btnAddManufacturer);
            grpPurchaseInfo.Controls.Add(cboGrain);
            grpPurchaseInfo.Controls.Add(cboCaliber);
            grpPurchaseInfo.Controls.Add(cboManufacturer);
            grpPurchaseInfo.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            grpPurchaseInfo.Location = new Point(12, 12);
            grpPurchaseInfo.Name = "grpPurchaseInfo";
            grpPurchaseInfo.Size = new Size(571, 400);
            grpPurchaseInfo.TabIndex = 0;
            grpPurchaseInfo.TabStop = false;
            grpPurchaseInfo.Text = "Purchase Information";
            // 
            // lblLotNumber
            // 
            lblLotNumber.AutoSize = true;
            lblLotNumber.Location = new Point(17, 333);
            lblLotNumber.Name = "lblLotNumber";
            lblLotNumber.Size = new Size(171, 21);
            lblLotNumber.TabIndex = 22;
            lblLotNumber.Text = "Lot Number (Optional):";
            // 
            // lblContainers
            // 
            lblContainers.AutoSize = true;
            lblContainers.Location = new Point(17, 297);
            lblContainers.Name = "lblContainers";
            lblContainers.Size = new Size(88, 21);
            lblContainers.TabIndex = 21;
            lblContainers.Text = "Containers:";
            // 
            // txtLotNumber
            // 
            txtLotNumber.Location = new Point(406, 330);
            txtLotNumber.Name = "txtLotNumber";
            txtLotNumber.Size = new Size(120, 29);
            txtLotNumber.TabIndex = 20;
            // 
            // nudContainers
            // 
            nudContainers.Location = new Point(406, 295);
            nudContainers.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudContainers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudContainers.Name = "nudContainers";
            nudContainers.Size = new Size(120, 29);
            nudContainers.TabIndex = 19;
            nudContainers.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblRoundsPerContainer
            // 
            lblRoundsPerContainer.AutoSize = true;
            lblRoundsPerContainer.Location = new Point(17, 262);
            lblRoundsPerContainer.Name = "lblRoundsPerContainer";
            lblRoundsPerContainer.Size = new Size(157, 21);
            lblRoundsPerContainer.TabIndex = 18;
            lblRoundsPerContainer.Text = "Round Per Container:";
            // 
            // nudRoundsPerContainer
            // 
            nudRoundsPerContainer.Location = new Point(406, 260);
            nudRoundsPerContainer.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudRoundsPerContainer.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudRoundsPerContainer.Name = "nudRoundsPerContainer";
            nudRoundsPerContainer.Size = new Size(120, 29);
            nudRoundsPerContainer.TabIndex = 17;
            nudRoundsPerContainer.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblTotalCost
            // 
            lblTotalCost.AutoSize = true;
            lblTotalCost.Location = new Point(17, 365);
            lblTotalCost.Name = "lblTotalCost";
            lblTotalCost.Size = new Size(80, 21);
            lblTotalCost.TabIndex = 16;
            lblTotalCost.Text = "Total Cost:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(406, 365);
            label1.Name = "label1";
            label1.Size = new Size(49, 21);
            label1.TabIndex = 15;
            label1.Text = "$0.00";
            // 
            // lblPricePerRound
            // 
            lblPricePerRound.AutoSize = true;
            lblPricePerRound.Location = new Point(17, 227);
            lblPricePerRound.Name = "lblPricePerRound";
            lblPricePerRound.Size = new Size(146, 21);
            lblPricePerRound.TabIndex = 14;
            lblPricePerRound.Text = "Price Per Round ($):";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.Location = new Point(17, 192);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(73, 21);
            lblQuantity.TabIndex = 13;
            lblQuantity.Text = "Quantity:";
            // 
            // lblPurchaseDate
            // 
            lblPurchaseDate.AutoSize = true;
            lblPurchaseDate.Location = new Point(17, 161);
            lblPurchaseDate.Name = "lblPurchaseDate";
            lblPurchaseDate.Size = new Size(112, 21);
            lblPurchaseDate.TabIndex = 12;
            lblPurchaseDate.Text = "Purchase Date:";
            // 
            // nudCostPerRound
            // 
            nudCostPerRound.DecimalPlaces = 2;
            nudCostPerRound.Location = new Point(406, 225);
            nudCostPerRound.Name = "nudCostPerRound";
            nudCostPerRound.Size = new Size(120, 29);
            nudCostPerRound.TabIndex = 11;
            // 
            // nudQuantity
            // 
            nudQuantity.Increment = new decimal(new int[] { 20, 0, 0, 0 });
            nudQuantity.Location = new Point(406, 190);
            nudQuantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudQuantity.Name = "nudQuantity";
            nudQuantity.Size = new Size(120, 29);
            nudQuantity.TabIndex = 10;
            // 
            // dtpPurchaseDate
            // 
            dtpPurchaseDate.Format = DateTimePickerFormat.Short;
            dtpPurchaseDate.Location = new Point(340, 155);
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.Size = new Size(186, 29);
            dtpPurchaseDate.TabIndex = 9;
            // 
            // lblGrain
            // 
            lblGrain.AutoSize = true;
            lblGrain.Location = new Point(17, 123);
            lblGrain.Name = "lblGrain";
            lblGrain.Size = new Size(51, 21);
            lblGrain.TabIndex = 8;
            lblGrain.Text = "Grain:";
            // 
            // lblCaliber
            // 
            lblCaliber.AutoSize = true;
            lblCaliber.Location = new Point(17, 88);
            lblCaliber.Name = "lblCaliber";
            lblCaliber.Size = new Size(62, 21);
            lblCaliber.TabIndex = 7;
            lblCaliber.Text = "Caliber:";
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.Location = new Point(17, 53);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(107, 21);
            lblManufacturer.TabIndex = 6;
            lblManufacturer.Text = "Manufacturer:";
            // 
            // btnAddGrain
            // 
            btnAddGrain.Location = new Point(532, 120);
            btnAddGrain.Name = "btnAddGrain";
            btnAddGrain.Size = new Size(29, 29);
            btnAddGrain.TabIndex = 5;
            btnAddGrain.Text = "+";
            btnAddGrain.UseVisualStyleBackColor = true;
            btnAddGrain.Click += btnAddGrain_Click;
            // 
            // btnAddCaliber
            // 
            btnAddCaliber.Location = new Point(532, 84);
            btnAddCaliber.Name = "btnAddCaliber";
            btnAddCaliber.Size = new Size(29, 29);
            btnAddCaliber.TabIndex = 4;
            btnAddCaliber.Text = "+";
            btnAddCaliber.UseVisualStyleBackColor = true;
            btnAddCaliber.Click += btnAddCaliber_Click;
            // 
            // btnAddManufacturer
            // 
            btnAddManufacturer.Location = new Point(532, 49);
            btnAddManufacturer.Name = "btnAddManufacturer";
            btnAddManufacturer.Size = new Size(29, 29);
            btnAddManufacturer.TabIndex = 3;
            btnAddManufacturer.Text = "+";
            btnAddManufacturer.UseVisualStyleBackColor = true;
            btnAddManufacturer.Click += btnAddManufacturer_Click;
            // 
            // cboGrain
            // 
            cboGrain.FormattingEnabled = true;
            cboGrain.Location = new Point(339, 120);
            cboGrain.Name = "cboGrain";
            cboGrain.Size = new Size(187, 29);
            cboGrain.TabIndex = 2;
            // 
            // cboCaliber
            // 
            cboCaliber.FormattingEnabled = true;
            cboCaliber.Location = new Point(339, 85);
            cboCaliber.Name = "cboCaliber";
            cboCaliber.Size = new Size(187, 29);
            cboCaliber.TabIndex = 1;
            // 
            // cboManufacturer
            // 
            cboManufacturer.FormattingEnabled = true;
            cboManufacturer.Location = new Point(163, 50);
            cboManufacturer.Name = "cboManufacturer";
            cboManufacturer.Size = new Size(363, 29);
            cboManufacturer.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Font = new Font("Segoe UI", 12F);
            btnCancel.Location = new Point(508, 418);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 37);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 12F);
            btnAdd.Location = new Point(427, 418);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 37);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // PurchaseWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(592, 462);
            ControlBox = false;
            Controls.Add(btnAdd);
            Controls.Add(btnCancel);
            Controls.Add(grpPurchaseInfo);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "PurchaseWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AmmoTracker [Add Purchase Window]";
            grpPurchaseInfo.ResumeLayout(false);
            grpPurchaseInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudContainers).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRoundsPerContainer).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCostPerRound).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudQuantity).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpPurchaseInfo;
        private Button btnAddManufacturer;
        private ComboBox cboGrain;
        private ComboBox cboCaliber;
        private ComboBox cboManufacturer;
        private Label lblGrain;
        private Label lblCaliber;
        private Label lblManufacturer;
        private Button btnAddGrain;
        private Button btnAddCaliber;
        private NumericUpDown nudCostPerRound;
        private NumericUpDown nudQuantity;
        private DateTimePicker dtpPurchaseDate;
        private Label lblPricePerRound;
        private Label lblQuantity;
        private Label lblPurchaseDate;
        private Label lblTotalCost;
        private Label label1;
        private Button btnCancel;
        private Button btnAdd;
        private NumericUpDown nudRoundsPerContainer;
        private Label lblRoundsPerContainer;
        private NumericUpDown nudContainers;
        private TextBox txtLotNumber;
        private Label lblLotNumber;
        private Label lblContainers;
    }
}
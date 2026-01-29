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
            dataGridView1 = new DataGridView();
            cboType = new ComboBox();
            txtSearch = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            lblCount = new Label();
            lblTotal = new Label();
            lblAlert = new Label();
            dtpDate = new DateTimePicker();
            nudRounds = new NumericUpDown();
            nudCost = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRounds).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCost).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(602, 146);
            dataGridView1.Margin = new Padding(4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(309, 210);
            dataGridView1.TabIndex = 0;
            // 
            // cboType
            // 
            cboType.FormattingEnabled = true;
            cboType.Location = new Point(602, 50);
            cboType.Margin = new Padding(4);
            cboType.Name = "cboType";
            cboType.Size = new Size(307, 29);
            cboType.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(721, 426);
            txtSearch.Margin = new Padding(4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(127, 29);
            txtSearch.TabIndex = 2;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(71, 381);
            btnAdd.Margin = new Padding(4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(96, 32);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(175, 381);
            btnUpdate.Margin = new Padding(4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(96, 32);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(279, 381);
            btnDelete.Margin = new Padding(4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(96, 32);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(564, 435);
            btnRefresh.Margin = new Padding(4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(96, 32);
            btnRefresh.TabIndex = 8;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Location = new Point(184, 109);
            lblCount.Margin = new Padding(4, 0, 4, 0);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(52, 21);
            lblCount.TabIndex = 9;
            lblCount.Text = "label1";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(185, 59);
            lblTotal.Margin = new Padding(4, 0, 4, 0);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(52, 21);
            lblTotal.TabIndex = 10;
            lblTotal.Text = "label2";
            // 
            // lblAlert
            // 
            lblAlert.AutoSize = true;
            lblAlert.Location = new Point(350, 55);
            lblAlert.Margin = new Padding(4, 0, 4, 0);
            lblAlert.Name = "lblAlert";
            lblAlert.Size = new Size(53, 21);
            lblAlert.TabIndex = 11;
            lblAlert.Text = "{Alert}";
            // 
            // dtpDate
            // 
            dtpDate.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(404, 182);
            dtpDate.Margin = new Padding(4);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(142, 29);
            dtpDate.TabIndex = 12;
            // 
            // nudRounds
            // 
            nudRounds.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudRounds.Location = new Point(358, 298);
            nudRounds.Name = "nudRounds";
            nudRounds.Size = new Size(77, 29);
            nudRounds.TabIndex = 13;
            nudRounds.TextAlign = HorizontalAlignment.Center;
            // 
            // nudCost
            // 
            nudCost.DecimalPlaces = 2;
            nudCost.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            nudCost.Location = new Point(358, 335);
            nudCost.Name = "nudCost";
            nudCost.Size = new Size(77, 29);
            nudCost.TabIndex = 14;
            nudCost.TextAlign = HorizontalAlignment.Center;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1029, 630);
            Controls.Add(nudCost);
            Controls.Add(nudRounds);
            Controls.Add(dtpDate);
            Controls.Add(lblAlert);
            Controls.Add(lblTotal);
            Controls.Add(lblCount);
            Controls.Add(btnRefresh);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(txtSearch);
            Controls.Add(cboType);
            Controls.Add(dataGridView1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AmmoTracker";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRounds).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCost).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private ComboBox cboType;
        private TextBox txtSearch;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private Label lblCount;
        private Label lblTotal;
        private Label lblAlert;
        private DateTimePicker dtpDate;
        private NumericUpDown nudRounds;
        private NumericUpDown nudCost;
    }
}

namespace CodeTimeTracker
{
    partial class NewCodeObjectWindow
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
            txtCodeObjectName = new TextBox();
            lblCodeObjectName = new Label();
            lblCodeObjectType = new Label();
            cmbCodeObjectType = new ComboBox();
            btnCreate = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // txtCodeObjectName
            // 
            txtCodeObjectName.Location = new Point(115, 37);
            txtCodeObjectName.Name = "txtCodeObjectName";
            txtCodeObjectName.Size = new Size(257, 23);
            txtCodeObjectName.TabIndex = 3;
            // 
            // lblCodeObjectName
            // 
            lblCodeObjectName.AutoSize = true;
            lblCodeObjectName.Location = new Point(12, 40);
            lblCodeObjectName.Name = "lblCodeObjectName";
            lblCodeObjectName.Size = new Size(42, 15);
            lblCodeObjectName.TabIndex = 2;
            lblCodeObjectName.Text = "Name:";
            // 
            // lblCodeObjectType
            // 
            lblCodeObjectType.AutoSize = true;
            lblCodeObjectType.Location = new Point(12, 84);
            lblCodeObjectType.Name = "lblCodeObjectType";
            lblCodeObjectType.Size = new Size(90, 15);
            lblCodeObjectType.TabIndex = 4;
            lblCodeObjectType.Text = "Type (optional):";
            // 
            // cmbCodeObjectType
            // 
            cmbCodeObjectType.FormattingEnabled = true;
            cmbCodeObjectType.Location = new Point(115, 81);
            cmbCodeObjectType.Name = "cmbCodeObjectType";
            cmbCodeObjectType.Size = new Size(257, 23);
            cmbCodeObjectType.TabIndex = 5;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(216, 126);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 7;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(297, 126);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // NewCodeObjectWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(380, 161);
            Controls.Add(btnCreate);
            Controls.Add(btnCancel);
            Controls.Add(cmbCodeObjectType);
            Controls.Add(lblCodeObjectType);
            Controls.Add(txtCodeObjectName);
            Controls.Add(lblCodeObjectName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewCodeObjectWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New CodeObject";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCodeObjectName;
        private Label lblCodeObjectName;
        private Label lblCodeObjectType;
        private ComboBox cmbCodeObjectType;
        private Button btnCreate;
        private Button btnCancel;
    }
}
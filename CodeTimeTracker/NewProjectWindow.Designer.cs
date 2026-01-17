namespace CodeTimeTracker
{
    partial class NewProjectWindow
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
            lblProjectName = new Label();
            txtProjectName = new TextBox();
            btnCancel = new Button();
            btnCreate = new Button();
            SuspendLayout();
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Location = new Point(12, 40);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(42, 15);
            lblProjectName.TabIndex = 0;
            lblProjectName.Text = "Name:";
            // 
            // txtProjectName
            // 
            txtProjectName.Location = new Point(115, 37);
            txtProjectName.Name = "txtProjectName";
            txtProjectName.Size = new Size(257, 23);
            txtProjectName.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(297, 80);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(216, 80);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(75, 23);
            btnCreate.TabIndex = 3;
            btnCreate.Text = "Create";
            btnCreate.UseVisualStyleBackColor = true;
            // 
            // NewProjectWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(384, 111);
            Controls.Add(btnCreate);
            Controls.Add(btnCancel);
            Controls.Add(txtProjectName);
            Controls.Add(lblProjectName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewProjectWindow";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New Project";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProjectName;
        private TextBox txtProjectName;
        private Button btnCancel;
        private Button btnCreate;
    }
}
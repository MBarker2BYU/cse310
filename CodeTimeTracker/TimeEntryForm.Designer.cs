using CodeTimeTracker.Controls;

namespace CodeTimeTracker
{
    partial class TimeEntryForm
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
            lblProject = new Label();
            cmbProject = new ComboBox();
            lblCodeObject = new Label();
            cmbCodeObject = new ComboBox();
            lblTaskName = new Label();
            txtTaskName = new TextBox();
            lblStart = new Label();
            startPicker = new AdvDateTimePicker();
            lblEnd = new Label();
            endPicker = new AdvDateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();
            btnNewCodeObject = new Button();
            btnNewProject = new Button();
            SuspendLayout();
            // 
            // lblProject
            // 
            lblProject.AutoSize = true;
            lblProject.Location = new Point(20, 20);
            lblProject.Name = "lblProject";
            lblProject.Size = new Size(47, 15);
            lblProject.TabIndex = 0;
            lblProject.Text = "Project:";
            // 
            // cmbProject
            // 
            cmbProject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProject.FormattingEnabled = true;
            cmbProject.Location = new Point(120, 17);
            cmbProject.Name = "cmbProject";
            cmbProject.Size = new Size(300, 23);
            cmbProject.TabIndex = 1;
            // 
            // lblCodeObject
            // 
            lblCodeObject.AutoSize = true;
            lblCodeObject.Location = new Point(20, 55);
            lblCodeObject.Name = "lblCodeObject";
            lblCodeObject.Size = new Size(76, 15);
            lblCodeObject.TabIndex = 2;
            lblCodeObject.Text = "Code Object:";
            // 
            // cmbCodeObject
            // 
            cmbCodeObject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCodeObject.FormattingEnabled = true;
            cmbCodeObject.Location = new Point(120, 52);
            cmbCodeObject.Name = "cmbCodeObject";
            cmbCodeObject.Size = new Size(300, 23);
            cmbCodeObject.TabIndex = 3;
            // 
            // lblTaskName
            // 
            lblTaskName.AutoSize = true;
            lblTaskName.Location = new Point(20, 90);
            lblTaskName.Name = "lblTaskName";
            lblTaskName.Size = new Size(68, 15);
            lblTaskName.TabIndex = 4;
            lblTaskName.Text = "Task Name:";
            // 
            // txtTaskName
            // 
            txtTaskName.Location = new Point(120, 87);
            txtTaskName.Name = "txtTaskName";
            txtTaskName.Size = new Size(300, 23);
            txtTaskName.TabIndex = 5;
            // 
            // lblStart
            // 
            lblStart.AutoSize = true;
            lblStart.Location = new Point(20, 125);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(64, 15);
            lblStart.TabIndex = 6;
            lblStart.Text = "Start Time:";
            // 
            // startPicker
            // 
            startPicker.Location = new Point(120, 122);
            startPicker.MaximumSize = new Size(252, 28);
            startPicker.MinimumSize = new Size(252, 28);
            startPicker.Name = "startPicker";
            startPicker.Size = new Size(252, 28);
            startPicker.TabIndex = 7;
            startPicker.Value = new DateTime(2026, 1, 15, 23, 0, 21, 860);
            // 
            // lblEnd
            // 
            lblEnd.AutoSize = true;
            lblEnd.Location = new Point(20, 161);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(60, 15);
            lblEnd.TabIndex = 8;
            lblEnd.Text = "End Time:";
            // 
            // endPicker
            // 
            endPicker.Location = new Point(120, 156);
            endPicker.MaximumSize = new Size(252, 28);
            endPicker.MinimumSize = new Size(252, 28);
            endPicker.Name = "endPicker";
            endPicker.Size = new Size(252, 28);
            endPicker.TabIndex = 9;
            endPicker.Value = new DateTime(2026, 1, 15, 23, 0, 21, 856);
            // 
            // btnSave
            // 
            btnSave.BackColor = SystemColors.ControlDark;
            btnSave.FlatStyle = FlatStyle.System;
            btnSave.Font = new Font("Segoe UI", 10F);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(320, 199);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 28);
            btnSave.TabIndex = 12;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = SystemColors.ControlDark;
            btnCancel.FlatStyle = FlatStyle.System;
            btnCancel.Font = new Font("Segoe UI", 10F);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(426, 199);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 28);
            btnCancel.TabIndex = 13;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnNewCodeObject
            // 
            btnNewCodeObject.Location = new Point(426, 50);
            btnNewCodeObject.Name = "btnNewCodeObject";
            btnNewCodeObject.Size = new Size(100, 28);
            btnNewCodeObject.TabIndex = 15;
            btnNewCodeObject.Text = "New Object...";
            btnNewCodeObject.UseVisualStyleBackColor = true;
            btnNewCodeObject.Click += btnNewCodeObject_Click;
            // 
            // btnNewProject
            // 
            btnNewProject.Location = new Point(426, 15);
            btnNewProject.Name = "btnNewProject";
            btnNewProject.Size = new Size(100, 28);
            btnNewProject.TabIndex = 14;
            btnNewProject.Text = "New Project...";
            btnNewProject.UseVisualStyleBackColor = true;
            btnNewProject.Click += btnNewProject_Click;
            // 
            // TimeEntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(538, 237);
            Controls.Add(btnNewCodeObject);
            Controls.Add(btnNewProject);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(endPicker);
            Controls.Add(lblEnd);
            Controls.Add(startPicker);
            Controls.Add(lblStart);
            Controls.Add(txtTaskName);
            Controls.Add(lblTaskName);
            Controls.Add(cmbCodeObject);
            Controls.Add(lblCodeObject);
            Controls.Add(cmbProject);
            Controls.Add(lblProject);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TimeEntryForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Time Entry";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.ComboBox cmbProject;
        private System.Windows.Forms.Label lblCodeObject;
        private System.Windows.Forms.ComboBox cmbCodeObject;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.Label lblStart;
        private AdvDateTimePicker startPicker;
        private System.Windows.Forms.Label lblEnd;
        private AdvDateTimePicker endPicker;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private Button btnNewCodeObject;
        private Button btnNewProject;
    }
}
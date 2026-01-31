namespace AmmoTracker
{
    partial class MinimumThreshold
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
            nudMinimumThreshold = new NumericUpDown();
            lblMinimumThreshold = new Label();
            btnCancel = new Button();
            btn_Update = new Button();
            ((System.ComponentModel.ISupportInitialize)nudMinimumThreshold).BeginInit();
            SuspendLayout();
            // 
            // nudMinimumThreshold
            // 
            nudMinimumThreshold.Location = new Point(206, 20);
            nudMinimumThreshold.Margin = new Padding(4);
            nudMinimumThreshold.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudMinimumThreshold.Name = "nudMinimumThreshold";
            nudMinimumThreshold.Size = new Size(110, 29);
            nudMinimumThreshold.TabIndex = 0;
            nudMinimumThreshold.TextAlign = HorizontalAlignment.Center;
            nudMinimumThreshold.Value = new decimal(new int[] { 100000, 0, 0, 0 });
            // 
            // lblMinimumThreshold
            // 
            lblMinimumThreshold.AutoSize = true;
            lblMinimumThreshold.Location = new Point(12, 22);
            lblMinimumThreshold.Name = "lblMinimumThreshold";
            lblMinimumThreshold.Size = new Size(154, 21);
            lblMinimumThreshold.TabIndex = 1;
            lblMinimumThreshold.Text = "Minimum Threshold:";
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.OK;
            btnCancel.Location = new Point(241, 65);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 35);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btn_Update
            // 
            btn_Update.Location = new Point(160, 65);
            btn_Update.Name = "btn_Update";
            btn_Update.Size = new Size(75, 35);
            btn_Update.TabIndex = 3;
            btn_Update.Text = "Update";
            btn_Update.UseVisualStyleBackColor = true;
            btn_Update.Click += btn_Update_Click;
            // 
            // MinimumThreshold
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(329, 112);
            Controls.Add(btn_Update);
            Controls.Add(btnCancel);
            Controls.Add(lblMinimumThreshold);
            Controls.Add(nudMinimumThreshold);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(4);
            Name = "MinimumThreshold";
            StartPosition = FormStartPosition.CenterParent;
            Text = "AmmoTracker [Minimum Threshold]";
            ((System.ComponentModel.ISupportInitialize)nudMinimumThreshold).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NumericUpDown nudMinimumThreshold;
        private Label lblMinimumThreshold;
        private Button btnCancel;
        private Button btn_Update;
    }
}
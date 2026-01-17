namespace CodeTimeTracker
{
    partial class MainWindow
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
            components = new System.ComponentModel.Container();
            lblProject = new Label();
            cmbProject = new ComboBox();
            btnNewProject = new Button();
            lblCodeObject = new Label();
            cmbCodeObject = new ComboBox();
            btnNewCodeObject = new Button();
            lblTask = new Label();
            txtTaskName = new TextBox();
            btnStart = new Button();
            btnStop = new Button();
            btnPause = new Button();
            lblCurrentStatus = new Label();
            dgvEntries = new DataGridView();
            lblTotalTime = new Label();
            btnExportFullTxt = new Button();
            btnExportFullCsv = new Button();
            btnExportProject = new Button();
            chkShowDeleted = new CheckBox();
            contextMenuGrid = new ContextMenuStrip(components);
            editEntryToolStripMenuItem = new ToolStripMenuItem();
            deleteEntryToolStripMenuItem = new ToolStripMenuItem();
            restoreEntryToolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem1 = new ToolStripSeparator();
            addManualEntryToolStripMenuItem = new ToolStripMenuItem();
            mnuMain = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuData = new ToolStripMenuItem();
            mnuExport = new ToolStripMenuItem();
            mnuExportProject = new ToolStripMenuItem();
            mnuExportFull = new ToolStripMenuItem();
            mnuExportFullText = new ToolStripMenuItem();
            mnuExportFullCSV = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            mnuReset = new ToolStripMenuItem();
            mnuSep = new ToolStripSeparator();
            mnuExit = new ToolStripMenuItem();
            mnuHelp = new ToolStripMenuItem();
            mnuAbout = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgvEntries).BeginInit();
            contextMenuGrid.SuspendLayout();
            mnuMain.SuspendLayout();
            SuspendLayout();
            // 
            // lblProject
            // 
            lblProject.AutoSize = true;
            lblProject.Location = new Point(12, 55);
            lblProject.Name = "lblProject";
            lblProject.Size = new Size(47, 15);
            lblProject.TabIndex = 1;
            lblProject.Text = "Project:";
            // 
            // cmbProject
            // 
            cmbProject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProject.FormattingEnabled = true;
            cmbProject.Location = new Point(100, 52);
            cmbProject.Name = "cmbProject";
            cmbProject.Size = new Size(300, 23);
            cmbProject.TabIndex = 2;
            cmbProject.SelectedIndexChanged += cmbProject_SelectedIndexChanged;
            // 
            // btnNewProject
            // 
            btnNewProject.Location = new Point(410, 50);
            btnNewProject.Name = "btnNewProject";
            btnNewProject.Size = new Size(100, 28);
            btnNewProject.TabIndex = 3;
            btnNewProject.Text = "New Project...";
            btnNewProject.UseVisualStyleBackColor = true;
            btnNewProject.Click += btnNewProject_Click;
            // 
            // lblCodeObject
            // 
            lblCodeObject.AutoSize = true;
            lblCodeObject.Location = new Point(12, 90);
            lblCodeObject.Name = "lblCodeObject";
            lblCodeObject.Size = new Size(76, 15);
            lblCodeObject.TabIndex = 4;
            lblCodeObject.Text = "Code Object:";
            // 
            // cmbCodeObject
            // 
            cmbCodeObject.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCodeObject.FormattingEnabled = true;
            cmbCodeObject.Location = new Point(100, 87);
            cmbCodeObject.Name = "cmbCodeObject";
            cmbCodeObject.Size = new Size(300, 23);
            cmbCodeObject.TabIndex = 5;
            // 
            // btnNewCodeObject
            // 
            btnNewCodeObject.Location = new Point(410, 85);
            btnNewCodeObject.Name = "btnNewCodeObject";
            btnNewCodeObject.Size = new Size(100, 28);
            btnNewCodeObject.TabIndex = 6;
            btnNewCodeObject.Text = "New Object...";
            btnNewCodeObject.UseVisualStyleBackColor = true;
            btnNewCodeObject.Click += btnNewCodeObject_Click;
            // 
            // lblTask
            // 
            lblTask.AutoSize = true;
            lblTask.Location = new Point(12, 125);
            lblTask.Name = "lblTask";
            lblTask.Size = new Size(68, 15);
            lblTask.TabIndex = 7;
            lblTask.Text = "Task Name:";
            // 
            // txtTaskName
            // 
            txtTaskName.Location = new Point(100, 122);
            txtTaskName.Name = "txtTaskName";
            txtTaskName.Size = new Size(410, 23);
            txtTaskName.TabIndex = 8;
            // 
            // btnStart
            // 
            btnStart.BackColor = SystemColors.ControlDark;
            btnStart.FlatStyle = FlatStyle.System;
            btnStart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStart.ForeColor = Color.White;
            btnStart.Location = new Point(100, 160);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(120, 40);
            btnStart.TabIndex = 9;
            btnStart.Text = "START";
            btnStart.UseVisualStyleBackColor = false;
            btnStart.Click += btnStart_Click;
            // 
            // btnStop
            // 
            btnStop.BackColor = SystemColors.ControlDark;
            btnStop.FlatStyle = FlatStyle.System;
            btnStop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnStop.ForeColor = Color.White;
            btnStop.Location = new Point(242, 160);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(120, 40);
            btnStop.TabIndex = 10;
            btnStop.Text = "STOP";
            btnStop.UseVisualStyleBackColor = false;
            btnStop.Click += btnStop_Click;
            // 
            // btnPause
            // 
            btnPause.BackColor = SystemColors.ControlDark;
            btnPause.FlatStyle = FlatStyle.System;
            btnPause.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnPause.ForeColor = Color.White;
            btnPause.Location = new Point(390, 160);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(120, 40);
            btnPause.TabIndex = 11;
            btnPause.Text = "PAUSE";
            btnPause.UseVisualStyleBackColor = false;
            btnPause.Click += btnPause_Click;
            // 
            // lblCurrentStatus
            // 
            lblCurrentStatus.AutoSize = true;
            lblCurrentStatus.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblCurrentStatus.ForeColor = Color.DarkSlateGray;
            lblCurrentStatus.Location = new Point(100, 210);
            lblCurrentStatus.Name = "lblCurrentStatus";
            lblCurrentStatus.Size = new Size(106, 15);
            lblCurrentStatus.TabIndex = 12;
            lblCurrentStatus.Text = "No timer running...";
            // 
            // dgvEntries
            // 
            dgvEntries.AllowUserToAddRows = false;
            dgvEntries.AllowUserToDeleteRows = false;
            dgvEntries.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEntries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEntries.Location = new Point(12, 240);
            dgvEntries.Name = "dgvEntries";
            dgvEntries.ReadOnly = true;
            dgvEntries.RowHeadersWidth = 51;
            dgvEntries.Size = new Size(776, 300);
            dgvEntries.TabIndex = 13;
            dgvEntries.CellMouseDown += dgvEntries_CellMouseDown;
            // 
            // lblTotalTime
            // 
            lblTotalTime.AutoSize = true;
            lblTotalTime.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalTime.Location = new Point(12, 550);
            lblTotalTime.Name = "lblTotalTime";
            lblTotalTime.Size = new Size(143, 19);
            lblTotalTime.TabIndex = 14;
            lblTotalTime.Text = "Total Time: 00:00:00";
            // 
            // btnExportFullTxt
            // 
            btnExportFullTxt.Location = new Point(500, 545);
            btnExportFullTxt.Name = "btnExportFullTxt";
            btnExportFullTxt.Size = new Size(130, 30);
            btnExportFullTxt.TabIndex = 15;
            btnExportFullTxt.Text = "Export Full (TXT)";
            btnExportFullTxt.UseVisualStyleBackColor = true;
            btnExportFullTxt.Click += btnExportFullTxt_Click;
            // 
            // btnExportFullCsv
            // 
            btnExportFullCsv.Location = new Point(640, 545);
            btnExportFullCsv.Name = "btnExportFullCsv";
            btnExportFullCsv.Size = new Size(130, 30);
            btnExportFullCsv.TabIndex = 16;
            btnExportFullCsv.Text = "Export Full (CSV)";
            btnExportFullCsv.UseVisualStyleBackColor = true;
            btnExportFullCsv.Click += btnExportFullCsv_Click;
            // 
            // btnExportProject
            // 
            btnExportProject.Location = new Point(360, 545);
            btnExportProject.Name = "btnExportProject";
            btnExportProject.Size = new Size(130, 30);
            btnExportProject.TabIndex = 17;
            btnExportProject.Text = "Export Project";
            btnExportProject.UseVisualStyleBackColor = true;
            btnExportProject.Click += btnExportProject_Click;
            // 
            // chkShowDeleted
            // 
            chkShowDeleted.Location = new Point(627, 210);
            chkShowDeleted.Name = "chkShowDeleted";
            chkShowDeleted.Size = new Size(161, 24);
            chkShowDeleted.TabIndex = 0;
            chkShowDeleted.Text = "Show Deleted Entries";
            chkShowDeleted.TextAlign = ContentAlignment.MiddleCenter;
            chkShowDeleted.CheckedChanged += chkShowDeleted_CheckedChanged;
            // 
            // contextMenuGrid
            // 
            contextMenuGrid.Items.AddRange(new ToolStripItem[] { editEntryToolStripMenuItem, deleteEntryToolStripMenuItem, restoreEntryToolStripMenuItem, toolStripMenuItem1, addManualEntryToolStripMenuItem });
            contextMenuGrid.Name = "contextMenuGrid";
            contextMenuGrid.Size = new Size(140, 98);
            // 
            // editEntryToolStripMenuItem
            // 
            editEntryToolStripMenuItem.Name = "editEntryToolStripMenuItem";
            editEntryToolStripMenuItem.Size = new Size(139, 22);
            editEntryToolStripMenuItem.Text = "Edit";
            editEntryToolStripMenuItem.Click += editEntryToolStripMenuItem_Click;
            // 
            // deleteEntryToolStripMenuItem
            // 
            deleteEntryToolStripMenuItem.Name = "deleteEntryToolStripMenuItem";
            deleteEntryToolStripMenuItem.Size = new Size(139, 22);
            deleteEntryToolStripMenuItem.Text = "Delete";
            deleteEntryToolStripMenuItem.Click += deleteEntryToolStripMenuItem_Click;
            // 
            // restoreEntryToolStripMenuItem
            // 
            restoreEntryToolStripMenuItem.Name = "restoreEntryToolStripMenuItem";
            restoreEntryToolStripMenuItem.Size = new Size(139, 22);
            restoreEntryToolStripMenuItem.Text = "Restore";
            restoreEntryToolStripMenuItem.Click += restoreEntryToolStripMenuItem_Click;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(136, 6);
            // 
            // addManualEntryToolStripMenuItem
            // 
            addManualEntryToolStripMenuItem.Name = "addManualEntryToolStripMenuItem";
            addManualEntryToolStripMenuItem.Size = new Size(139, 22);
            addManualEntryToolStripMenuItem.Text = "Manual Add";
            addManualEntryToolStripMenuItem.Click += addManualEntryToolStripMenuItem_Click;
            // 
            // mnuMain
            // 
            mnuMain.Items.AddRange(new ToolStripItem[] { mnuFile, mnuHelp });
            mnuMain.Location = new Point(0, 0);
            mnuMain.Name = "mnuMain";
            mnuMain.Size = new Size(800, 24);
            mnuMain.TabIndex = 18;
            mnuMain.Text = "Main Menu";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuData, mnuSep, mnuExit });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(37, 20);
            mnuFile.Text = "&File";
            // 
            // mnuData
            // 
            mnuData.DropDownItems.AddRange(new ToolStripItem[] { mnuExport, toolStripMenuItem2, mnuReset });
            mnuData.Name = "mnuData";
            mnuData.Size = new Size(180, 22);
            mnuData.Text = "&Data";
            // 
            // mnuExport
            // 
            mnuExport.DropDownItems.AddRange(new ToolStripItem[] { mnuExportProject, mnuExportFull });
            mnuExport.Name = "mnuExport";
            mnuExport.Size = new Size(180, 22);
            mnuExport.Text = "&Export";
            // 
            // mnuExportProject
            // 
            mnuExportProject.Name = "mnuExportProject";
            mnuExportProject.Size = new Size(180, 22);
            mnuExportProject.Text = "&Project";
            mnuExportProject.Click += btnExportProject_Click;
            // 
            // mnuExportFull
            // 
            mnuExportFull.DropDownItems.AddRange(new ToolStripItem[] { mnuExportFullText, mnuExportFullCSV });
            mnuExportFull.Name = "mnuExportFull";
            mnuExportFull.Size = new Size(180, 22);
            mnuExportFull.Text = "Full";
            // 
            // mnuExportFullText
            // 
            mnuExportFullText.Name = "mnuExportFullText";
            mnuExportFullText.Size = new Size(180, 22);
            mnuExportFullText.Text = "As Text";
            mnuExportFullText.Click += btnExportFullTxt_Click;
            // 
            // mnuExportFullCSV
            // 
            mnuExportFullCSV.Name = "mnuExportFullCSV";
            mnuExportFullCSV.Size = new Size(180, 22);
            mnuExportFullCSV.Text = "As CSV";
            mnuExportFullCSV.Click += btnExportFullCsv_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(177, 6);
            // 
            // mnuReset
            // 
            mnuReset.Name = "mnuReset";
            mnuReset.Size = new Size(180, 22);
            mnuReset.Text = "&Reset";
            mnuReset.Click += mnuReset_Click;
            // 
            // mnuSep
            // 
            mnuSep.Name = "mnuSep";
            mnuSep.Size = new Size(177, 6);
            // 
            // mnuExit
            // 
            mnuExit.Name = "mnuExit";
            mnuExit.Size = new Size(180, 22);
            mnuExit.Text = "E&xit";
            mnuExit.Click += mnuExit_Click;
            // 
            // mnuHelp
            // 
            mnuHelp.Alignment = ToolStripItemAlignment.Right;
            mnuHelp.DropDownItems.AddRange(new ToolStripItem[] { mnuAbout });
            mnuHelp.Name = "mnuHelp";
            mnuHelp.Size = new Size(44, 20);
            mnuHelp.Text = "&Help";
            // 
            // mnuAbout
            // 
            mnuAbout.Name = "mnuAbout";
            mnuAbout.Size = new Size(107, 22);
            mnuAbout.Text = "&About";
            mnuAbout.Click += mnuAbout_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 590);
            Controls.Add(mnuMain);
            Controls.Add(chkShowDeleted);
            Controls.Add(btnExportProject);
            Controls.Add(btnExportFullCsv);
            Controls.Add(btnExportFullTxt);
            Controls.Add(lblTotalTime);
            Controls.Add(dgvEntries);
            Controls.Add(lblCurrentStatus);
            Controls.Add(btnPause);
            Controls.Add(btnStop);
            Controls.Add(btnStart);
            Controls.Add(txtTaskName);
            Controls.Add(lblTask);
            Controls.Add(btnNewCodeObject);
            Controls.Add(cmbCodeObject);
            Controls.Add(lblCodeObject);
            Controls.Add(btnNewProject);
            Controls.Add(cmbProject);
            Controls.Add(lblProject);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MainMenuStrip = mnuMain;
            MaximizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CodeTime Tracker";
            ((System.ComponentModel.ISupportInitialize)dgvEntries).EndInit();
            contextMenuGrid.ResumeLayout(false);
            mnuMain.ResumeLayout(false);
            mnuMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblProject;
        private System.Windows.Forms.ComboBox cmbProject;
        private System.Windows.Forms.Button btnNewProject;
        private System.Windows.Forms.Label lblCodeObject;
        private System.Windows.Forms.ComboBox cmbCodeObject;
        private System.Windows.Forms.Button btnNewCodeObject;
        private System.Windows.Forms.Label lblTask;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Label lblCurrentStatus;
        private System.Windows.Forms.DataGridView dgvEntries;
        private System.Windows.Forms.Label lblTotalTime;
        private System.Windows.Forms.Button btnExportFullTxt;
        private System.Windows.Forms.Button btnExportFullCsv;
        private System.Windows.Forms.Button btnExportProject;
        private System.Windows.Forms.CheckBox chkShowDeleted;
        private ContextMenuStrip contextMenuGrid;
        private ToolStripMenuItem editEntryToolStripMenuItem;
        private ToolStripMenuItem deleteEntryToolStripMenuItem;
        private ToolStripMenuItem restoreEntryToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem addManualEntryToolStripMenuItem;
        private MenuStrip mnuMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuHelp;
        private ToolStripSeparator mnuSep;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuExport;
        private ToolStripMenuItem mnuAbout;
        private ToolStripMenuItem mnuExportProject;
        private ToolStripMenuItem mnuExportFull;
        private ToolStripMenuItem mnuExportFullText;
        private ToolStripMenuItem mnuExportFullCSV;
        private ToolStripMenuItem mnuData;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem mnuReset;
    }
}
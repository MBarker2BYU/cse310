namespace SwarNet.Forms
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
            btnHost = new Button();
            btnFindGames = new Button();
            listBoxHosts = new ListBox();
            btnConnect = new Button();
            txtChatInput = new TextBox();
            btnSendChat = new Button();
            rtbLog = new RichTextBox();
            lblHosts = new Label();
            fleetAttackBoard = new SwarNet.Controls.FleetGameBoard();
            fleetStatusBoard = new SwarNet.Controls.FleetGameBoard();
            lblTurn = new Label();
            tabControl1 = new TabControl();
            tpChat = new TabPage();
            richTextBoxChat = new RichTextBox();
            tpLog = new TabPage();
            lblLogTitle = new Label();
            tabControl1.SuspendLayout();
            tpChat.SuspendLayout();
            tpLog.SuspendLayout();
            SuspendLayout();
            // 
            // btnHost
            // 
            btnHost.FlatAppearance.BorderColor = Color.WhiteSmoke;
            btnHost.ForeColor = SystemColors.ControlText;
            btnHost.Location = new Point(10, 11);
            btnHost.Name = "btnHost";
            btnHost.Size = new Size(131, 38);
            btnHost.TabIndex = 0;
            btnHost.Text = "Host Game";
            btnHost.UseVisualStyleBackColor = true;
            btnHost.Click += btnHost_Click;
            // 
            // btnFindGames
            // 
            btnFindGames.Location = new Point(147, 11);
            btnFindGames.Name = "btnFindGames";
            btnFindGames.Size = new Size(131, 38);
            btnFindGames.TabIndex = 1;
            btnFindGames.Text = "Find Games";
            btnFindGames.UseVisualStyleBackColor = true;
            btnFindGames.Click += btnFindGames_Click;
            // 
            // listBoxHosts
            // 
            listBoxHosts.FormattingEnabled = true;
            listBoxHosts.ItemHeight = 15;
            listBoxHosts.Location = new Point(10, 78);
            listBoxHosts.Name = "listBoxHosts";
            listBoxHosts.Size = new Size(268, 124);
            listBoxHosts.TabIndex = 2;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(284, 78);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(131, 38);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtChatInput
            // 
            txtChatInput.Location = new Point(6, 207);
            txtChatInput.Name = "txtChatInput";
            txtChatInput.Size = new Size(301, 23);
            txtChatInput.TabIndex = 4;
            // 
            // btnSendChat
            // 
            btnSendChat.Enabled = false;
            btnSendChat.Location = new Point(313, 203);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(100, 28);
            btnSendChat.TabIndex = 5;
            btnSendChat.Text = "Send";
            btnSendChat.UseVisualStyleBackColor = true;
            btnSendChat.Click += btnSendChat_Click;
            // 
            // rtbLog
            // 
            rtbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbLog.Location = new Point(6, 21);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(407, 218);
            rtbLog.TabIndex = 6;
            rtbLog.Text = "";
            // 
            // lblHosts
            // 
            lblHosts.AutoSize = true;
            lblHosts.Location = new Point(10, 60);
            lblHosts.Name = "lblHosts";
            lblHosts.Size = new Size(135, 15);
            lblHosts.TabIndex = 8;
            lblHosts.Text = "Available SwarNet Hosts";
            // 
            // fleetAttackBoard
            // 
            fleetAttackBoard.BackColor = Color.FromArgb(15, 15, 35);
            fleetAttackBoard.HoverEnabled = false;
            fleetAttackBoard.IsAttackBoard = true;
            fleetAttackBoard.Location = new Point(846, 51);
            fleetAttackBoard.Name = "fleetAttackBoard";
            fleetAttackBoard.OverlayMessage = null;
            fleetAttackBoard.Size = new Size(400, 400);
            fleetAttackBoard.TabIndex = 9;
            // 
            // fleetStatusBoard
            // 
            fleetStatusBoard.BackColor = Color.FromArgb(15, 15, 35);
            fleetStatusBoard.HoverEnabled = false;
            fleetStatusBoard.IsAttackBoard = false;
            fleetStatusBoard.Location = new Point(440, 51);
            fleetStatusBoard.Name = "fleetStatusBoard";
            fleetStatusBoard.OverlayMessage = null;
            fleetStatusBoard.Size = new Size(400, 400);
            fleetStatusBoard.TabIndex = 10;
            // 
            // lblTurn
            // 
            lblTurn.AutoSize = true;
            lblTurn.Font = new Font("Comic Sans MS", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTurn.ForeColor = SystemColors.ControlText;
            lblTurn.Location = new Point(440, 17);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(84, 23);
            lblTurn.TabIndex = 11;
            lblTurn.Text = "Standby...";
            // 
            // tabControl1
            // 
            tabControl1.Alignment = TabAlignment.Bottom;
            tabControl1.Controls.Add(tpChat);
            tabControl1.Controls.Add(tpLog);
            tabControl1.Location = new Point(7, 208);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(427, 273);
            tabControl1.TabIndex = 12;
            // 
            // tpChat
            // 
            tpChat.Controls.Add(richTextBoxChat);
            tpChat.Controls.Add(txtChatInput);
            tpChat.Controls.Add(btnSendChat);
            tpChat.Location = new Point(4, 4);
            tpChat.Name = "tpChat";
            tpChat.Padding = new Padding(3);
            tpChat.Size = new Size(419, 245);
            tpChat.TabIndex = 1;
            tpChat.Text = "Chat";
            tpChat.UseVisualStyleBackColor = true;
            // 
            // richTextBoxChat
            // 
            richTextBoxChat.Location = new Point(6, 6);
            richTextBoxChat.Name = "richTextBoxChat";
            richTextBoxChat.ReadOnly = true;
            richTextBoxChat.Size = new Size(407, 191);
            richTextBoxChat.TabIndex = 0;
            richTextBoxChat.Text = "";
            // 
            // tpLog
            // 
            tpLog.Controls.Add(rtbLog);
            tpLog.Controls.Add(lblLogTitle);
            tpLog.Location = new Point(4, 4);
            tpLog.Name = "tpLog";
            tpLog.Padding = new Padding(3);
            tpLog.Size = new Size(419, 245);
            tpLog.TabIndex = 0;
            tpLog.Text = "Comm Logs";
            tpLog.UseVisualStyleBackColor = true;
            // 
            // lblLogTitle
            // 
            lblLogTitle.AutoSize = true;
            lblLogTitle.Location = new Point(6, 3);
            lblLogTitle.Name = "lblLogTitle";
            lblLogTitle.Size = new Size(117, 15);
            lblLogTitle.TabIndex = 7;
            lblLogTitle.Text = "Communication Log";
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1275, 504);
            Controls.Add(tabControl1);
            Controls.Add(lblTurn);
            Controls.Add(fleetAttackBoard);
            Controls.Add(fleetStatusBoard);
            Controls.Add(lblHosts);
            Controls.Add(btnConnect);
            Controls.Add(listBoxHosts);
            Controls.Add(btnFindGames);
            Controls.Add(btnHost);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SwarNet – Sea Warfare Network by ShadowWorx Systems";
            FormClosing += FormMain_FormClosing;
            tabControl1.ResumeLayout(false);
            tpChat.ResumeLayout(false);
            tpChat.PerformLayout();
            tpLog.ResumeLayout(false);
            tpLog.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHost;
        private System.Windows.Forms.Button btnFindGames;
        private System.Windows.Forms.ListBox listBoxHosts;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtChatInput;
        private System.Windows.Forms.Button btnSendChat;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Label lblHosts;
        private Controls.FleetGameBoard fleetAttackBoard;
        private Controls.FleetGameBoard fleetStatusBoard;
        private Label lblTurn;
        private TabControl tabControl1;
        private TabPage tpLog;
        private TabPage tpChat;
        private Label lblLogTitle;
        private RichTextBox richTextBoxChat;
    }
}
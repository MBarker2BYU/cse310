namespace SwarNet
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
            lblLogTitle = new Label();
            lblHosts = new Label();
            ownBoardPanel = new GameBoardPanel();
            opponentBoardPanel = new GameBoardPanel();
            button1 = new Button();
            SuspendLayout();
            // 
            // btnHost
            // 
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
            listBoxHosts.Location = new Point(10, 54);
            listBoxHosts.Name = "listBoxHosts";
            listBoxHosts.Size = new Size(268, 124);
            listBoxHosts.TabIndex = 2;
            // 
            // btnConnect
            // 
            btnConnect.Location = new Point(284, 54);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(131, 38);
            btnConnect.TabIndex = 3;
            btnConnect.Text = "Connect";
            btnConnect.UseVisualStyleBackColor = true;
            btnConnect.Click += btnConnect_Click;
            // 
            // txtChatInput
            // 
            txtChatInput.Location = new Point(10, 184);
            txtChatInput.Name = "txtChatInput";
            txtChatInput.Size = new Size(268, 23);
            txtChatInput.TabIndex = 4;
            // 
            // btnSendChat
            // 
            btnSendChat.Enabled = false;
            btnSendChat.Location = new Point(284, 182);
            btnSendChat.Name = "btnSendChat";
            btnSendChat.Size = new Size(131, 28);
            btnSendChat.TabIndex = 5;
            btnSendChat.Text = "Send Chat";
            btnSendChat.UseVisualStyleBackColor = true;
            btnSendChat.Click += btnSendChat_Click;
            // 
            // rtbLog
            // 
            rtbLog.Location = new Point(10, 210);
            rtbLog.Name = "rtbLog";
            rtbLog.ReadOnly = true;
            rtbLog.Size = new Size(405, 241);
            rtbLog.TabIndex = 6;
            rtbLog.Text = "";
            // 
            // lblLogTitle
            // 
            lblLogTitle.AutoSize = true;
            lblLogTitle.Location = new Point(10, 195);
            lblLogTitle.Name = "lblLogTitle";
            lblLogTitle.Size = new Size(117, 15);
            lblLogTitle.TabIndex = 7;
            lblLogTitle.Text = "Communication Log";
            // 
            // lblHosts
            // 
            lblHosts.AutoSize = true;
            lblHosts.Location = new Point(10, 39);
            lblHosts.Name = "lblHosts";
            lblHosts.Size = new Size(135, 15);
            lblHosts.TabIndex = 8;
            lblHosts.Text = "Available SwarNet Hosts";
            // 
            // ownBoardPanel
            // 
            ownBoardPanel.BackColor = Color.FromArgb(20, 20, 40);
            ownBoardPanel.IsReadOnly = false;
            ownBoardPanel.Location = new Point(443, 12);
            ownBoardPanel.Name = "ownBoardPanel";
            ownBoardPanel.Size = new Size(410, 439);
            ownBoardPanel.TabIndex = 9;
            // 
            // opponentBoardPanel
            // 
            opponentBoardPanel.BackColor = Color.FromArgb(20, 20, 40);
            opponentBoardPanel.IsReadOnly = false;
            opponentBoardPanel.Location = new Point(869, 12);
            opponentBoardPanel.Name = "opponentBoardPanel";
            opponentBoardPanel.Size = new Size(410, 439);
            opponentBoardPanel.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(312, 123);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 11;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1306, 463);
            Controls.Add(button1);
            Controls.Add(opponentBoardPanel);
            Controls.Add(ownBoardPanel);
            Controls.Add(lblHosts);
            Controls.Add(lblLogTitle);
            Controls.Add(rtbLog);
            Controls.Add(btnSendChat);
            Controls.Add(txtChatInput);
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
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.Label lblHosts;
        private GameBoardPanel ownBoardPanel;
        private GameBoardPanel opponentBoardPanel;
        private Button button1;
    }
}
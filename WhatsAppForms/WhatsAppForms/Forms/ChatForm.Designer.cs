namespace WhatsAppForms
{
    partial class ChatForm
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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.usersPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chatsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chatBoxPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.sendBtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.usersPanel);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(768, 398);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Contacts";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // usersPanel
            // 
            this.usersPanel.AutoScroll = true;
            this.usersPanel.AutoSize = true;
            this.usersPanel.Location = new System.Drawing.Point(3, 3);
            this.usersPanel.Name = "usersPanel";
            this.usersPanel.Size = new System.Drawing.Size(762, 392);
            this.usersPanel.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chatsPanel);
            this.tabPage1.Controls.Add(this.chatBoxPanel);
            this.tabPage1.Controls.Add(this.sendBtn);
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 398);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chats";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chatsPanel
            // 
            this.chatsPanel.AutoScroll = true;
            this.chatsPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatsPanel.Location = new System.Drawing.Point(6, 6);
            this.chatsPanel.Name = "chatsPanel";
            this.chatsPanel.Size = new System.Drawing.Size(190, 386);
            this.chatsPanel.TabIndex = 5;
            this.chatsPanel.WrapContents = false;
            this.chatsPanel.Click += new System.EventHandler(this.chatsPanel_Click);
            // 
            // chatBoxPanel
            // 
            this.chatBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatBoxPanel.AutoScroll = true;
            this.chatBoxPanel.BackColor = System.Drawing.Color.Transparent;
            this.chatBoxPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.chatBoxPanel.Location = new System.Drawing.Point(202, 6);
            this.chatBoxPanel.Name = "chatBoxPanel";
            this.chatBoxPanel.Size = new System.Drawing.Size(560, 307);
            this.chatBoxPanel.TabIndex = 4;
            this.chatBoxPanel.WrapContents = false;
            // 
            // sendBtn
            // 
            this.sendBtn.Enabled = false;
            this.sendBtn.Location = new System.Drawing.Point(689, 319);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(73, 73);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(202, 319);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(481, 73);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 426);
            this.tabControl1.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.Name = "ChatForm";
            this.Text = "WhatsApp Forms";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TabPage tabPage3;
        private FlowLayoutPanel usersPanel;
        private TabPage tabPage1;
        private FlowLayoutPanel chatsPanel;
        private FlowLayoutPanel chatBoxPanel;
        private Button sendBtn;
        private RichTextBox richTextBox1;
        private TabControl tabControl1;
    }
}
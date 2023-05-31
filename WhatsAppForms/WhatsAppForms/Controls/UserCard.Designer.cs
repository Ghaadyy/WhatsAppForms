namespace WhatsAppForms
{
    partial class UserCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.username = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // username
            // 
            this.username.AutoSize = true;
            this.username.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.username.Location = new System.Drawing.Point(3, 25);
            this.username.MaximumSize = new System.Drawing.Size(0, 30);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(71, 30);
            this.username.TabIndex = 0;
            this.username.Text = "Name";
            this.username.Click += new System.EventHandler(this.username_Click);
            // 
            // UserCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.Controls.Add(this.username);
            this.MaximumSize = new System.Drawing.Size(0, 80);
            this.MinimumSize = new System.Drawing.Size(0, 80);
            this.Name = "UserCard";
            this.Size = new System.Drawing.Size(77, 80);
            this.Click += new System.EventHandler(this.UserCard_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label username;
    }
}

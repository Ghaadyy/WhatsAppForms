namespace WhatsAppForms
{
    partial class SignUpForm
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
            this.firstNameInput = new System.Windows.Forms.TextBox();
            this.lastNameInput = new System.Windows.Forms.TextBox();
            this.usernameInput = new System.Windows.Forms.TextBox();
            this.passwordInput = new System.Windows.Forms.TextBox();
            this.signUp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // firstNameInput
            // 
            this.firstNameInput.Location = new System.Drawing.Point(338, 87);
            this.firstNameInput.Name = "firstNameInput";
            this.firstNameInput.Size = new System.Drawing.Size(100, 23);
            this.firstNameInput.TabIndex = 0;
            // 
            // lastNameInput
            // 
            this.lastNameInput.Location = new System.Drawing.Point(338, 144);
            this.lastNameInput.Name = "lastNameInput";
            this.lastNameInput.PlaceholderText = "Last Name";
            this.lastNameInput.Size = new System.Drawing.Size(100, 23);
            this.lastNameInput.TabIndex = 1;
            // 
            // usernameInput
            // 
            this.usernameInput.Location = new System.Drawing.Point(338, 200);
            this.usernameInput.Name = "usernameInput";
            this.usernameInput.PlaceholderText = "Username";
            this.usernameInput.Size = new System.Drawing.Size(100, 23);
            this.usernameInput.TabIndex = 2;
            // 
            // passwordInput
            // 
            this.passwordInput.Location = new System.Drawing.Point(338, 258);
            this.passwordInput.Name = "passwordInput";
            this.passwordInput.PasswordChar = '*';
            this.passwordInput.PlaceholderText = "Password";
            this.passwordInput.Size = new System.Drawing.Size(100, 23);
            this.passwordInput.TabIndex = 3;
            // 
            // signUp
            // 
            this.signUp.Location = new System.Drawing.Point(338, 318);
            this.signUp.Name = "signUp";
            this.signUp.Size = new System.Drawing.Size(100, 23);
            this.signUp.TabIndex = 4;
            this.signUp.Text = "Sign Up";
            this.signUp.UseVisualStyleBackColor = true;
            this.signUp.Click += new System.EventHandler(this.signUp_Click);
            // 
            // SignUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.signUp);
            this.Controls.Add(this.passwordInput);
            this.Controls.Add(this.usernameInput);
            this.Controls.Add(this.lastNameInput);
            this.Controls.Add(this.firstNameInput);
            this.Name = "SignUpForm";
            this.Text = "SignUpForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SignUpForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox firstNameInput;
        private TextBox lastNameInput;
        private TextBox usernameInput;
        private TextBox passwordInput;
        private Button signUp;
    }
}
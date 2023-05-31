namespace WhatsAppForms
{
    public partial class UserCard : UserControl
    {
        public string Username;

        public event EventHandler? UserSelected;

        public UserCard(string Username)
        {
            this.Username = Username;
            InitializeComponent();
            this.username.Text = Username;
        }

        private void UserCard_Click(object sender, EventArgs e)
        {
            OnUserSelected();
        }

        protected virtual void OnUserSelected()
        {
            if (UserSelected != null)
            {
                UserSelected(this, EventArgs.Empty);
            }
        }

        private void username_Click(object sender, EventArgs e)
        {
            OnUserSelected();
        }
    }
}

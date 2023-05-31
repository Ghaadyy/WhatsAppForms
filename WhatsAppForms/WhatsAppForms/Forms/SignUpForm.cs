using WhatsAppForms.Modules;

namespace WhatsAppForms
{
    public partial class SignUpForm : Form
    {
        private EventHandler onCloseHandler;
        private bool closedProgrammatically = false;

        public SignUpForm(EventHandler onCloseHandler)
        {
            this.onCloseHandler = onCloseHandler;
            InitializeComponent();
            this.AcceptButton = signUp;
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            string FirstName = firstNameInput.Text;
            string LastName = lastNameInput.Text;
            string Username = usernameInput.Text;
            string Password = Validator.ComputeSha256Hash(passwordInput.Text);

            try
            {
                User loggedInUser = User.SignUp(FirstName, LastName, Username, Password);
                closedProgrammatically = true;
                this.Close();

                ChatForm form = new ChatForm(loggedInUser);
                form.OnCloseForm += onCloseHandler;
                form.Show();
                MainForm.forms.Add(form);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void SignUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closedProgrammatically)
            {
                onCloseHandler(null, EventArgs.Empty);
            }
        }
    }
}

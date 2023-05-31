using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using WhatsAppForms.Modules;

namespace WhatsAppForms
{
    public partial class LoginForm : Form
    {
        private EventHandler onCloseHandler;
        private bool closedProgrammatically = false;

        public LoginForm(EventHandler onCloseHandler)
        {
            this.onCloseHandler = onCloseHandler;
            InitializeComponent();
            this.AcceptButton = login;
        }

        private void login_Click(object sender, EventArgs e)
        {
            string Username = usernameInput.Text;
            string Password = Validator.ComputeSha256Hash(passwordInput.Text);

            try
            {
                User loggedInUser = User.Login(Username, Password);
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

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closedProgrammatically)
            {
                onCloseHandler(null, EventArgs.Empty);
            }
        }
    }
}

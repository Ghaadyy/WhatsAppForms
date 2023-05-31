using System.Net.Sockets;
using WhatsAppForms.Modules;

namespace WhatsAppForms
{
    public partial class MainForm : Form
    {
        public static bool isRunning = true;
        public static List<Form> forms = new List<Form>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void signUp_Click(object sender, EventArgs e)
        {
            SignUpForm form = new SignUpForm(onCloseHandler);
            form.Show();
            this.Hide();
        }

        private void login_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm(onCloseHandler);
            form.Show();
            this.Hide();
        }

        private void onCloseHandler(object? sender, EventArgs e)
        {
            this.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            User.LoadUsers();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Form form in forms)
                form.Close();
        }
    }
}

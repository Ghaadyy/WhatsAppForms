using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhatsAppForms
{
    public partial class ChatItem : UserControl
    {
        public string UserName;
        public string Message;
        public string ID;

        public event EventHandler? chatSelected;

        public ChatItem(string UserName, string Message, string ID)
        {
            this.UserName = UserName;
            this.Message = Message;
            this.ID = ID;
            InitializeComponent();
        }

        private void ChatItem_Load(object sender, EventArgs e)
        {
            label1.Text = this.UserName;
            label2.Text = this.Message;

            foreach (Control control in this.Controls)
                control.Click += ChatItem_Click;
        }

        private void ChatItem_Click(object? sender, EventArgs e)
        {
            OnChatSelected();
        }

        protected virtual void OnChatSelected()
        {
            if (chatSelected != null)
            {
                chatSelected(this, EventArgs.Empty);
            }
        }
    }
}

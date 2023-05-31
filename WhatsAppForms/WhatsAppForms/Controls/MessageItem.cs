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
    public partial class MessageItem : UserControl
    {
        private DateTime MessageTime;
        private string MessageText;

        public bool IsSender = false;

        public MessageItem(string MessageText, DateTime MessageTime, bool IsSender)
        {
            this.MessageText = MessageText;
            this.MessageTime = MessageTime;
            this.IsSender = IsSender;
            InitializeComponent();

            if (IsSender)
            {
                this.BackColor = Color.LightGray;
            }
        }

        private void MessageItem_Load(object sender, EventArgs e)
        {
            label2.Text = MessageTime.ToString("HH:mm");
            label1.Text = MessageText;
        }
    }
}

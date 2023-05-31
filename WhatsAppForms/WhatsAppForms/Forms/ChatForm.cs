using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;
using WhatsAppForms.Modules;

namespace WhatsAppForms
{
    public partial class ChatForm : Form
    {
        private Conversation? conversation;
        private User user;
        private SocketClient client = new SocketClient();
        private List<Thread> activeThreads = new List<Thread>();
        public event EventHandler? OnCloseForm;

        public ChatForm(User user)
        {
            this.user = user;
            InitializeComponent();
            this.AcceptButton = sendBtn;
            MainForm.isRunning = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conversation.LoadConversations();
            user.LoadConversations();

            this.LoadConversations();
            this.LoadUsersCards();

            client.ConnectToServer(user);

            Thread messagesThread = new Thread(MonitorMessages);
            messagesThread.Start();
            activeThreads.Add(messagesThread);
        }

        private void LoadUsersCards()
        {
            foreach (string Username in User.Users.Keys)
            {
                if (Username != user.Username)
                {
                    UserCard card = new UserCard(Username);

                    card.UserSelected += (object? sender, EventArgs e) => OnSelectUser(Username);

                    usersPanel.Controls.Add(card);
                }
            }
        }

        private void OnSelectUser(string Username)
        {
            if (!Conversation.Conversations.Values.Any(conv =>
                conv.User1_Username == user.Username && conv.User2_Username == Username ||
                conv.User2_Username == user.Username && conv.User1_Username == Username
            ))
            {
                Conversation conversation = new Conversation(this.user.Username, Username);
                Conversation.Conversations.Add(conversation.ID, conversation);

                user.conversations.Add(conversation);
                User.Users[Username].conversations.Add(conversation);

                chatsPanel.Controls.Clear();
                this.LoadConversations();
            }
        }

        private void LoadConversations()
        {
            foreach (Conversation conversation in user.conversations)
            {
                Modules.Message? lastMessage = conversation.messages.LastOrDefault();

                string lastMessageText = lastMessage != null ? lastMessage.Text : "No messages";
                string UsernameText = conversation.User2_Username == user.Username ? conversation.User1_Username : conversation.User2_Username;

                ChatItem item = new ChatItem(UsernameText, lastMessageText, conversation.ID);

                EventHandler handler = (object? sender, EventArgs e) =>
                {
                    this.OnChatSelectedHandler(conversation);

                    List<ChatItem> chatItems = chatsPanel.Controls.OfType<ChatItem>().ToList();

                    foreach (ChatItem item in chatItems)
                        item.BackColor = Color.WhiteSmoke;

                    if (chatItems.Count > 0)
                    {
                        ChatItem chatItem = chatItems.Single(c => c.ID == conversation.ID);
                        chatItem.BackColor = Color.LightGray;
                    }
                };

                item.chatSelected += handler;
                chatsPanel.Controls.Add(item);
            }
        }

        private void OnChatSelectedHandler(Conversation conversation)
        {
            this.conversation = conversation;
            this.LoadCurrentConversation();
        }

        private void LoadCurrentConversation()
        {
            chatBoxPanel.Controls.Clear();

            if (conversation != null)
            {
                Control control = new Control();
                control.Width = chatBoxPanel.Width - 40;
                control.Height = 0;
                chatBoxPanel.Controls.Add(control);

                RenderMessages();

                chatBoxPanel.VerticalScroll.Value = chatBoxPanel.VerticalScroll.Maximum;
                chatBoxPanel.PerformLayout();
            }
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            MessageItem message1 = new MessageItem(richTextBox1.Text, DateTime.Now, true);
            message1.Dock = DockStyle.Right;

            if (conversation != null)
            {
                try
                {
                    string receiver = conversation.User2_Username == user.Username ? conversation.User1_Username : conversation.User2_Username;
                    Modules.Message msg = new Modules.Message(richTextBox1.Text, DateTime.Now, user.Username, receiver, conversation.ID);

                    Packet packet = new Packet(Method.SendMessage, msg);
                    Conversation.Conversations[msg.ConversationID].messages.Add(msg);
                    string json = SocketClient.ConvertToJson(packet);
                    client.SendMessage(json);
                }
                catch (SocketException socketEx)
                {
                    string message = socketEx.Message + "\n\nPlease make sure the server is up and running then try again.";
                    MessageBox.Show(message, "Connection Error", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Sending Error", MessageBoxButtons.OK);
                }
            }

            chatBoxPanel.Controls.Add(message1);
            chatBoxPanel.VerticalScroll.Value = chatBoxPanel.VerticalScroll.Maximum;
            chatBoxPanel.PerformLayout();
            richTextBox1.Text = "";
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.conversation != null)
                sendBtn.Enabled = richTextBox1.Text != "";
        }

        private void chatsPanel_Click(object sender, EventArgs e)
        {
            chatBoxPanel.Controls.Clear();
            this.conversation = null;

            List<ChatItem> chatItems = chatsPanel.Controls.OfType<ChatItem>().ToList();

            foreach (ChatItem item in chatItems)
                item.BackColor = Color.WhiteSmoke;
        }

        private async void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.CloseConnection();

            MainForm.isRunning = false;

            List<Task> threadTasks = new List<Task>();

            foreach (Thread thread in activeThreads)
            {
                if (thread.IsAlive)
                {
                    Task task = Task.Run(() => thread.Join());
                    threadTasks.Add(task);
                }
            }

            await Task.WhenAll(threadTasks);

            if (this.OnCloseForm != null) this.OnCloseForm(null, EventArgs.Empty);
        }

        private void RenderMessage(Modules.Message message)
        {
            chatBoxPanel.Invoke(() =>
            {
                MessageItem message1 = new MessageItem(message.Text, DateTime.Now, false);

                chatBoxPanel.Controls.Add(message1);
                chatBoxPanel.VerticalScroll.Value = chatBoxPanel.VerticalScroll.Maximum;
                chatBoxPanel.PerformLayout();
            });
        }

        private void RenderMessages()
        {
            if (conversation != null)
            {
                foreach (Modules.Message message in conversation.messages)
                {
                    MessageItem message1 = new MessageItem(message.Text, message.MessageTime, user.Username == message.SenderUsername);

                    if (user.Username == message.SenderUsername)
                        message1.Dock = DockStyle.Right;

                    chatBoxPanel.Controls.Add(message1);
                }
            }
        }

        private void MonitorMessages()
        {
            var options = new JsonSerializerOptions { IncludeFields = true, WriteIndented = true };

            while (MainForm.isRunning)
            {
                try
                {
                    string? message = client.ReceiveMessages();

                    if (message != null)
                    {
                        Modules.Message? msg = JsonSerializer.Deserialize<Modules.Message>(message, options);

                        if (msg != null)
                        {
                            Conversation.Conversations[msg.ConversationID].messages.Add(msg);
                            if (conversation != null && msg.ConversationID == conversation.ID) RenderMessage(msg);
                        }
                    }
                }
                catch (SocketException socketEx)
                {
                    string message = socketEx.Message + "\n\nPlease make sure the server is up and running then try again.";
                    MessageBox.Show(message, "Connection Error", MessageBoxButtons.OK);
                    break;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                    break;
                }
            }

            // MessageBox.Show("Finished thread");
        }
    }
}
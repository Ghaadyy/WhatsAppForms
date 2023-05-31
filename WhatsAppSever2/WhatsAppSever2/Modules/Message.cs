namespace WhatsAppServer
{
    [Serializable]
    public class Message
    {
        public string Text;
        public DateTime MessageTime;

        public string SenderUsername;
        public string ReceiverUsername;

        public string ConversationID;

        public static List<Message> Messages = new List<Message>();

        public Message(string Text, DateTime MessageTime, string SenderUsername, string ReceiverUsername, string ConversationID)
        {
            this.Text = Text;
            this.MessageTime = MessageTime;
            this.SenderUsername = SenderUsername;
            this.ReceiverUsername = ReceiverUsername;
            this.ConversationID = ConversationID;
        }

        public void Send()
        {
            if (!Conversation.Conversations.ContainsKey(ConversationID))
            {
                Conversation conv = new Conversation(SenderUsername, ReceiverUsername);
                conv.ID = ConversationID;
                Conversation.Conversations.Add(conv.ID, conv);
                conv.Save();
            }

            Conversation conversation = Conversation.Conversations[ConversationID];
            Message.Messages.Add(this);
            conversation.messages.Add(this);
            string content = $"{Text},{MessageTime},{SenderUsername},{ReceiverUsername},{ConversationID}\n";
            File.AppendAllText("Messages.csv", content);
        }

        public static void LoadMessages()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Messages.csv"))
                {
                    string? line;
                    sr.ReadLine();

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] userData = line.Split(',');

                        Message msg = new Message(userData[0], DateTime.Parse(userData[1]), userData[2], userData[3], userData[4]);

                        Messages.Add(msg);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

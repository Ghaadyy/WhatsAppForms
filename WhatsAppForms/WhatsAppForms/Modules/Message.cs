namespace WhatsAppForms.Modules
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
    }
}

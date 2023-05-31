namespace WhatsAppServer
{
    public class Conversation
    {
        public string User1_Username;
        public string User2_Username;

        public string ID;

        public List<Message> messages = new List<Message>();

        public static Dictionary<string, Conversation> Conversations = new Dictionary<string, Conversation>();

        public Conversation(string User1_Username, string User2_Username)
        {
            this.User1_Username = User1_Username;
            this.User2_Username = User2_Username;
            this.ID = Guid.NewGuid().ToString();
        }

        public void Save()
        {
            File.AppendAllText("Conversations.csv", $"{User1_Username},{User2_Username},{ID}\n");
        }

        public static void LoadConversations()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Conversations.csv"))
                {
                    string? line;
                    sr.ReadLine();

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] convData = line.Split(',');

                        Conversation conversation = new Conversation(convData[0], convData[1]);

                        conversation.ID = convData[2];

                        Conversations.Add(conversation.ID, conversation);
                    }
                }

                Message.LoadMessages();

                foreach (Message message in Message.Messages)
                {
                    Conversation.Conversations[message.ConversationID].messages.Add(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

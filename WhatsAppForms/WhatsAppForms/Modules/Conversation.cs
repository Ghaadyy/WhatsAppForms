using System.Net.Sockets;

namespace WhatsAppForms.Modules
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

        public static void LoadConversations()
        {
            SocketClient client = new SocketClient();

            try
            {
                client.ConnectToServer();

                Packet packet = new Packet(Method.GetConversations, null);
                string json = SocketClient.ConvertToJson(packet);

                client.SendMessage(json);

                string? response = client.ReceiveMessages();

                if (response != null)
                {
                    Dictionary<string, Conversation>? conversations = SocketClient.ParseJson<Dictionary<string, Conversation>>(response);

                    if (conversations != null)
                    {
                        Conversations = conversations;
                    }
                }
            }
            catch (SocketException socketEx)
            {
                string message = socketEx.Message + "\n\nPlease make sure the server is up and running then try again.";
                MessageBox.Show(message, "Connection Error", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace, ex.Source, MessageBoxButtons.OK);
            }
            finally
            {
                client.CloseConnection();
            }
        }
    }
}

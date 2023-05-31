using System.Net.Sockets;
using System.Text.Json;

namespace WhatsAppForms.Modules
{
    public class User
    {
        public string FirstName;
        public string LastName;
        public string Password;
        public string Username;

        public List<Conversation> conversations = new List<Conversation>();

        public static Dictionary<string, User> Users = new Dictionary<string, User>();

        public User(string FirstName, string LastName, string Username, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Password = Password;
            this.Username = Username;
        }

        public void LoadConversations()
        {
            this.conversations = (from conv in Conversation.Conversations.Values
                                  where conv.User1_Username == Username || conv.User2_Username == Username
                                  select conv).ToList();
        }

        public static User SignUp(string FirstName, string LastName, string Username, string Password)
        {
            SocketClient client = new SocketClient();

            try
            {
                client.ConnectToServer();

                User user = new User(FirstName, LastName, Username, Password);
                Packet packet = new Packet(Method.SignUp, user);

                string json = SocketClient.ConvertToJson(packet);

                client.SendMessage(json);

                string? response = client.ReceiveMessages();

                if (response != null)
                {
                    Packet? resPacket = SocketClient.ParseJson<Packet>(response);

                    if (resPacket != null)
                    {
                        if (resPacket.method == Method.Error)
                        {
                            string message = ((JsonElement)resPacket.Payload!).Deserialize<string>(new JsonSerializerOptions { WriteIndented = true, IncludeFields = true })!;
                            throw new Exception(message);
                        }
                        else
                        {
                            User? loggedInUser = ((JsonElement)resPacket.Payload!).Deserialize<User>(new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
                            if (loggedInUser != null) return loggedInUser;
                            else throw new Exception("Error parsing user");
                        }
                    }

                    else throw new Exception("Error parsing user");
                }
                else throw new Exception("Couldn't receive response from server");
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            finally
            {
                client.CloseConnection();
            }
        }

        public static User Login(string Username, string Password)
        {
            SocketClient client = new SocketClient();

            try
            {
                client.ConnectToServer();

                User user = new User("", "", Username, Password);
                Packet packet = new Packet(Method.Login, user);

                string json = SocketClient.ConvertToJson(packet);

                client.SendMessage(json);

                string? response = client.ReceiveMessages();

                if (response != null)
                {
                    Packet? resPacket = SocketClient.ParseJson<Packet>(response);

                    if (resPacket != null)
                    {
                        if (resPacket.method == Method.Error)
                        {
                            string message = ((JsonElement)resPacket.Payload!).Deserialize<string>(new JsonSerializerOptions { WriteIndented = true, IncludeFields = true })!;
                            throw new Exception(message);
                        }
                        else
                        {
                            User? loggedInUser = ((JsonElement)resPacket.Payload!).Deserialize<User>(new JsonSerializerOptions { WriteIndented = true, IncludeFields = true });
                            if (loggedInUser != null) return loggedInUser;
                            else throw new Exception("Error parsing user");
                        }
                    }
                    else throw new Exception("Error parsing user");
                }
                else throw new Exception("Couldn't receive response from server");
            }
            catch (SocketException ex)
            {
                throw ex;
            }
            finally
            {
                client.CloseConnection();
            }
        }

        public static void LoadUsers()
        {
            SocketClient client = new SocketClient();

            try
            {
                client.ConnectToServer();

                Packet packet = new Packet(Method.GetUsers, null);
                string json = SocketClient.ConvertToJson(packet);

                client.SendMessage(json);

                string? response = client.ReceiveMessages();
                if (response != null)
                {
                    Dictionary<string, User>? users = SocketClient.ParseJson<Dictionary<string, User>>(response);

                    if (users != null)
                    {
                        Users = users;
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

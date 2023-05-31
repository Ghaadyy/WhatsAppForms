namespace WhatsAppServer
{
    public class User
    {
        public static Dictionary<string, User> Users = new Dictionary<string, User>();

        public string FirstName;
        public string LastName;
        public string Password;
        public string Username;

        public List<Conversation> conversations = new List<Conversation>();

        public User(string FirstName, string LastName, string Username, string Password)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Password = Password;
            this.Username = Username;
        }

        public void LoadConversations()
        {
            conversations = (from conv in Conversation.Conversations.Values
                             where conv.User1_Username == Username || conv.User2_Username == Username
                             select conv).ToList();
        }

        public static User SignUp(string FirstName, string LastName, string Username, string Password)
        {
            if (Users.ContainsKey(Username))
                throw new Exception("Username is already taken.");

            if (FirstName == "" || LastName == "" || Username == "")
                throw new Exception("All fields must be filled.");

            Validator.IsValidPassword(Password);

            User user = new User(FirstName, LastName, Username, Password);
            Users.Add(Username, user);
            File.AppendAllText("Users.csv", $"{user.FirstName},{user.LastName},{user.Username},{user.Password}\n");

            return user;
        }

        public static User Login(string Username, string Password)
        {
            if (Users.ContainsKey(Username))
            {
                if (Users[Username].Password == Password)
                {
                    return Users[Username];
                }
                else
                    throw new Exception("Incorrect password.");
            }
            else
            {
                throw new Exception("User doesn't exist.");
            }
        }

        public static void LoadUsers()
        {
            try
            {
                using (StreamReader sr = new StreamReader("Users.csv"))
                {
                    string? line;
                    sr.ReadLine();

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] userData = line.Split(',');

                        User user = new User(userData[0], userData[1], userData[2], userData[3]);

                        Users.Add(user.Username, user);
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

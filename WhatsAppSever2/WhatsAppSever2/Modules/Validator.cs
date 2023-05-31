using System.Security.Cryptography;
using System.Text;

namespace WhatsAppServer
{
    public static class Validator
    {
        public static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static bool IsValidPassword(string password)
        {
            if (password.Length < 6)
                throw new Exception("Password length should be greater than 6 characters.");
            if (password == "")
                throw new Exception("Password can't be empty.");

            return true;
        }

        public static bool IsValidEmail(string email)
        {
            return true;
        }
    }
}

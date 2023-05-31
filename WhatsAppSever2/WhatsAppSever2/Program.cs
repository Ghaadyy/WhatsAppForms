using System;
using System.Collections.Generic;
using System.Linq;

namespace WhatsAppServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Conversation.LoadConversations();
            User.LoadUsers();

            SocketServer server = new SocketServer();
            server.StartListening();
        }
    }
}
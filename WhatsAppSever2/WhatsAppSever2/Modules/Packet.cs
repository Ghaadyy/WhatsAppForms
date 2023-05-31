using System;

namespace WhatsAppServer
{
    public enum Method
    {
        Login, SignUp, SendMessage, GetUsers, GetConversations, Error
    }

    [Serializable]
    public class Packet
    {
        public Method method;
        public object? Payload;

        public Packet(Method method, object Payload)
        {
            this.method = method;
            this.Payload = Payload;
        }
    }
}


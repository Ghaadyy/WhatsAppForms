using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace WhatsAppServer
{
    public class SocketServer
    {
        private Socket listener;

        private Dictionary<string, Socket> connectedSockets = new Dictionary<string, Socket>();

        public void StartListening()
        {
            IPAddress ipAddress = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8888);

            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);

            Console.WriteLine("Socket server started. Waiting for connections...");

            MontiorOnlineUsers();

            while (true)
            {
                Socket clientSocket = listener.Accept();
                Console.WriteLine("Client connected: " + clientSocket.RemoteEndPoint);

                Thread thread = new Thread(() =>
                {
                    HandleClientMessages(clientSocket);
                });

                thread.Start();
            }
        }

        private void MontiorOnlineUsers()
        {
            Thread onlineUsersThread = new Thread(() =>
            {
                while (true)
                {
                    //Console.Clear();
                    Console.WriteLine("Connected sockets:");
                    foreach (string name in connectedSockets.Keys)
                        Console.WriteLine(name);
                    Console.WriteLine();
                    Thread.Sleep(2000);
                }
            });

            onlineUsersThread.Start();
        }

        private void SendMessage(Socket clientSocket, string message)
        {
            List<byte> dataList = Encoding.UTF8.GetBytes(message).ToList();
            dataList.Add(0);
            byte[] data = dataList.ToArray();
            int bytesSent = 0;
            const int bufferSize = 1024;

            while (bytesSent < data.Length)
            {
                int remainingBytes = data.Length - bytesSent;
                int bytesToSend = Math.Min(remainingBytes, bufferSize);
                byte[] buffer = new byte[bytesToSend];
                Array.Copy(data, bytesSent, buffer, 0, bytesToSend);
                int sent = clientSocket.Send(buffer);
                bytesSent += sent;
            }
        }

        private string? ReceiveMessage(Socket clientSocket)
        {
            const int bufferSize = 1024;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            string message = string.Empty;

            try
            {
                do
                {
                    bytesRead = clientSocket.Receive(buffer);

                    if (bytesRead > 0)
                    {
                        if (buffer.ToList().Contains(0))
                        {
                            message += Encoding.UTF8.GetString(buffer, 0, bytesRead - 1);
                            break;
                        }
                        else
                        {
                            message += Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        }
                    }
                } while (bytesRead > 0);

                return message == string.Empty ? null : message;
            }
            catch (SocketException)
            {
                return null;
            }
        }

        private void HandleClientMessages(Socket clientSocket)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };

            while (true)
            {
                try
                {
                    string? message = ReceiveMessage(clientSocket);

                    if (message != null)
                    {
                        // Console.WriteLine(message);

                        Packet? packet = JsonSerializer.Deserialize<Packet>(message, options);

                        if (packet != null)
                        {
                            if (packet.Payload != null)
                            {
                                JsonElement element = ((JsonElement)packet.Payload);

                                if (packet.method == Method.SendMessage)
                                {
                                    HandleMessage(clientSocket, element);
                                }

                                if (packet.method == Method.Login)
                                {
                                    HandleLogin(clientSocket, element);
                                }

                                if (packet.method == Method.SignUp)
                                {
                                    HandleSignUp(clientSocket, element);
                                }
                            }

                            else
                            {
                                if (packet.method == Method.GetUsers)
                                {
                                    HandleGetUsers(clientSocket);
                                }

                                if (packet.method == Method.GetConversations)
                                {
                                    HandleGetConversations(clientSocket);
                                }
                            }
                        }
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine("Client disconnected: " + clientSocket.RemoteEndPoint);

                    foreach (KeyValuePair<string, Socket> pair in connectedSockets)
                    {
                        if (connectedSockets[pair.Key] == clientSocket)
                        {
                            connectedSockets.Remove(pair.Key);
                        }
                        else
                            Console.WriteLine(pair.Key);
                    }

                    break;
                }
            }

            clientSocket.Close();
        }

        private void HandleGetUsers(Socket clientSocket)
        {
            string json;

            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };

            try
            {
                json = JsonSerializer.Serialize(User.Users, options);
                //Console.WriteLine(json);
                SendMessage(clientSocket, json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void HandleGetConversations(Socket clientSocket)
        {
            string json;

            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };

            try
            {
                json = JsonSerializer.Serialize(Conversation.Conversations, options);
                //Console.WriteLine(json);
                SendMessage(clientSocket, json);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void HandleLogin(Socket clientSocket, JsonElement packet)
        {
            Packet resPacket;
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };

            User? user = packet.Deserialize<User>(options);

            try
            {
                if (user != null)
                {
                    User loggedInUser = User.Login(user.Username, user.Password);
                    resPacket = new Packet(Method.Login, loggedInUser);
                    string json = JsonSerializer.Serialize(resPacket, options);

                    //Console.WriteLine(json);
                    SendMessage(clientSocket, json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                resPacket = new Packet(Method.Error, e.Message);
                string json = JsonSerializer.Serialize(resPacket, options);
                SendMessage(clientSocket, json);
            }
        }

        private void HandleSignUp(Socket clientSocket, JsonElement packet)
        {
            Packet resPacket;
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };

            User? user = packet.Deserialize<User>(options);

            try
            {
                if (user != null)
                {
                    User loggedInUser = User.SignUp(user.FirstName, user.LastName, user.Username, user.Password);
                    resPacket = new Packet(Method.SignUp, loggedInUser);
                    string json = JsonSerializer.Serialize(resPacket, options);

                    SendMessage(clientSocket, json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                resPacket = new Packet(Method.Error, e.Message);
                string json = JsonSerializer.Serialize(resPacket, options);
                SendMessage(clientSocket, json);
            }
        }

        private void HandleMessage(Socket clientSocket, JsonElement packet)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };

            Message? msg = packet.Deserialize<Message>(options);

            if (msg != null)
            {
                if (!connectedSockets.ContainsKey(msg.SenderUsername))
                {
                    connectedSockets.Add(msg.SenderUsername, clientSocket);
                }
                else
                {
                    connectedSockets[msg.SenderUsername] = clientSocket;
                }

                if (msg.ReceiverUsername != "")
                {
                    string json = JsonSerializer.Serialize(msg, options);
                    msg.Send();

                    if (connectedSockets.ContainsKey(msg.ReceiverUsername))
                    {
                        SendMessage(connectedSockets[msg.ReceiverUsername], json);
                    }
                }
            }
        }

        public void StopListening()
        {
            listener.Close();
        }
    }
}
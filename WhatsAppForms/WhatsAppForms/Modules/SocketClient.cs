using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WhatsAppForms.Modules
{
    public class SocketClient
    {
        public Socket? clientSocket;
        private const int bufferSize = 1024;

        public void ConnectToServer()
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.1.102");
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 8888);

            clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(remoteEndPoint);
        }

        public void ConnectToServer(User user)
        {
            IPAddress ipAddress = IPAddress.Parse("192.168.1.102");
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, 8888);

            clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(remoteEndPoint);
            Message msg = new Message("", DateTime.Now, user.Username, "", "");
            Packet packet = new Packet(Method.SendMessage, msg);
            string json = JsonSerializer.Serialize(packet, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
            this.SendMessage(json);
        }

        public void CloseConnection()
        {
            if (clientSocket == null)
                throw new Exception("Please connect to the server first.");
            clientSocket.Close();
        }

        public string? ReceiveMessages()
        {
            if (clientSocket == null)
                throw new Exception("Please connect to the server first.");

            byte[] buffer = new byte[bufferSize];
            int bytesRead;
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

        public void SendMessage(string message)
        {
            if (clientSocket == null)
                throw new Exception("Please connect to the server first.");

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

        public static string ConvertToJson(Packet packet)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            string json = JsonSerializer.Serialize(packet, options);
            return json;
        }

        public static T? ParseJson<T>(string json)
        {
            var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true };
            T? result = JsonSerializer.Deserialize<T>(json, options);
            return result;
        }
    }
}

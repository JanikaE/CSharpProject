using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ChatServer
{
    public class Program
    {
        private static Socket? server;
        private static Socket? client;

        private static readonly Dictionary<string, Socket> clients = new();

        public static void Main()
        {
            CreateServer();

            new Thread(() =>
            {
                try
                {
                    if (server != null)
                    {
                        Accept(server);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }).Start();
            new Thread(() =>
            {
                Thread.Sleep(500);
                Send();
            }).Start();
        }

        /// <summary>
        /// 服务器手动发送消息
        /// </summary>
        private static void Send()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("等待发送信息，回车->发送，close->断开连接");
                    string? s = Console.ReadLine();
                    if (s !=  null)
                    {
                        SendMessage(s);
                    }
                    Console.WriteLine("发送成功！\n");

                    if (s == "close")
                    {
                        client?.Close();
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// 将消息发送至所有客户端
        /// </summary>
        /// <param name="s"></param>
        private static void SendMessage(String s)
        {
            try
            {
                foreach (Socket client in clients.Values)
                {
                    client.Send(Encoding.Default.GetBytes(s));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreateServer()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress iPAddress = new(new byte[] { 127, 0, 0, 1 });
            int port = 8000;
            EndPoint endPoint = new IPEndPoint(iPAddress, port);
            server.Bind(endPoint);
            server.Listen(8);

            Console.WriteLine($"tcp端口已经打开,{endPoint}");
            DisplayAllClients();
        }

        private static void Accept(object obj)
        {
            Socket socket = (Socket)obj;
            while (true)
            {
                client = socket.Accept();
                string? ip = client.RemoteEndPoint?.ToString();
                if (ip == null)
                {
                    return;
                }

                if (!clients.ContainsKey(ip))
                {
                    clients.Add(ip, client);
                    Console.WriteLine(ip + "已连接");
                    DisplayAllClients();
                }

                new Thread(() =>
                {
                    try
                    {
                        Recieve(client);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        clients.Remove(ip);
                    }
                })
                { IsBackground = true }.Start();
            }
        }

        private static void Recieve(object obj)
        {
            Socket client = (Socket)obj;
            while (true)
            {
                try
                {
                    byte[] buffer = new byte[1024 * 1024 * 8];
                    int length = client.Receive(buffer);

                    string message = Encoding.Default.GetString(buffer, 0, length);
                    Console.WriteLine(client.RemoteEndPoint?.ToString() + "::" + message);
                    if (message == null || message.Length == 0 || message == "close")
                    {
                        string? ip = client.RemoteEndPoint?.ToString();
                        Console.WriteLine(ip + "连接断开！");
                        if (ip != null)
                        {
                            clients.Remove(ip);
                        }
                        DisplayAllClients();
                        break;
                    }
                    else
                    {
                        SendMessage(message);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    string? ip = client.RemoteEndPoint?.ToString();
                    if (ip != null)
                    {
                        clients.Remove(ip);
                    }
                    DisplayAllClients();
                    break;
                }
            }
        }

        /// <summary>
        /// 显示所有连接客户端
        /// </summary>
        static void DisplayAllClients()
        {
            if (clients.Values.Count == 0)
            {
                Console.WriteLine("目前无连接");
                Console.WriteLine("等待新的连接");
            }
            else
            {
                Console.WriteLine("目前所有连接");
                Console.WriteLine("+==========+");
                foreach (Socket client in clients.Values)
                {
                    Console.WriteLine(client.RemoteEndPoint?.ToString());
                }
                Console.WriteLine("+==========+");
                Console.WriteLine();
            }
        }
    }
}
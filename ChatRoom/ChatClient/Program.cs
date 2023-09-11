using System.Diagnostics;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace ChatClient
{
    public static class Program
    {
        private static Socket? client;
        private static string? name;

        public static void Main()
        {
            CreateClient();

            new Thread(() =>
            {
                Thread.Sleep(500);
                SendMessage();
            }).Start();

            new Thread(() =>
            {
                Thread.Sleep(500);
                ReceiveMessage();
            }).Start();

        }

        private static void CreateClient()
        {
            try
            {
                Console.WriteLine("设置名字：");
                name = Console.ReadLine();

                IPAddress iPAdress = new(new byte[] { 127, 0, 0, 1 });
                int port = 8000;
                IPEndPoint iPEndPoint = new(iPAdress, port);
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(iPEndPoint);
                Console.WriteLine("已经连接上服务器");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Process.GetCurrentProcess().Close();
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        private static void SendMessage()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("等待发送信息，回车->发送，close->断开连接");
                    string? s = Console.ReadLine();
                    client?.Send(Encoding.Default.GetBytes(name + ":" + s));
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
        /// 接收消息
        /// </summary>
        private static void ReceiveMessage()
        {
            try
            {
                while (true)
                {
                    byte[] buffer = new byte[1024 * 1024 * 8];
                    if (client == null)
                    {
                        return;
                    }
                    int length = client.Receive(buffer);
                    string msg = Encoding.Default.GetString(buffer, 0, length);
                    if (buffer.Length > 0)
                    {
                        if (msg.StartsWith(name + ":"))
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        }
                        Console.WriteLine(msg);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine("断开连接");
                        break;
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
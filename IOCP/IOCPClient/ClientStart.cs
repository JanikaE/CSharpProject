using IOCPProtocol;
using PENet;

namespace IOCPClientExample
{
    /// <summary>
    /// 控制台客户端
    /// </summary>
    class ClientStart
    {
        static void Main(string[] args)
        {
            IOCPNet<ClientToken, NetMsg> client = new();
            client.StartAsClient("127.0.0.1", 6666);
            while (true)
            {
                string ipt = Console.ReadLine();
                if (ipt == "quit")
                {
                    client.CloseClient();
                    break;
                }
                else
                {
                    NetMsg msg = new()
                    {
                        msg = ipt
                    };

                    client.token.SendMsg(msg);
                }
            }
        }
    }
}
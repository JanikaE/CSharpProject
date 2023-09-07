using IOCPProtocol;
using PENet;

namespace IOCPServerExample
{
    /// <summary>
    /// 控制台服务器
    /// </summary>
    class ServerStart
    {
        static void Main(string[] args)
        {
            IOCPNet<ServerToken, NetMsg> server = new();
            server.StartAsServer("127.0.0.1", 6666, 100);

            while (true)
            {
                string ipt = Console.ReadLine();
                if (ipt == "quit")
                {
                    server.CloseServer();
                }
                else
                {
                    List<ServerToken> tokenList = server.GetTokenList();
                    for (int i = 0; i < tokenList.Count; i++)
                    {
                        tokenList[i].SendMsg(new NetMsg()
                        {
                            msg = string.Format("Broadcast:{0}", ipt)
                        });
                    }
                }
            }
        }
    }
}
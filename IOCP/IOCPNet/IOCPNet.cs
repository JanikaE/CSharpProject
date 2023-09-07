using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace PENet
{
    /// <summary>
    /// 基于IOCP封装的异步套接字通信、服务端与客户端
    /// </summary>
    public class IOCPNet<T, K>
        where T : IOCPToken<K>, new()
        where K : IOCPMsg, new()
    {
        private Socket skt;
        private SocketAsyncEventArgs saea;

        public IOCPNet()
        {
            saea = new SocketAsyncEventArgs();
            saea.Completed += new System.EventHandler<SocketAsyncEventArgs>(IO_Completed);
        }

        #region Server
        private int currentConnCount = 0;
        private Semaphore acceptSemaphore;
        public const int backlog = 100;
        private IOCPTokenPool<T, K> pool;
        private List<T> tokenList;

        public void StartAsServer(string ip, int port, int maxConnCnt)
        {
            currentConnCount = 0;
            acceptSemaphore = new Semaphore(maxConnCnt, maxConnCnt);
            pool = new IOCPTokenPool<T, K>(maxConnCnt);
            for (int i = 0; i < maxConnCnt; i++)
            {
                T token = new T()
                {
                    tokenID = i
                };
                pool.Push(token);
            }
            tokenList = new List<T>();

            IPEndPoint pt = new IPEndPoint(IPAddress.Parse(ip), port);
            skt = new Socket(pt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            skt.Bind(pt);
            skt.Listen(backlog);
            IOCPTool.ColorLog("Server Start...", LogColor.Green);
            StartAccept();
        }

        private void StartAccept()
        {
            saea.AcceptSocket = null;
            acceptSemaphore.WaitOne();
            bool suspend = skt.AcceptAsync(saea);
            if (!suspend)
            {
                IOCPTool.ColorLog("连接成功", LogColor.Green);
                ProcessAccept();
            }
        }

        private void ProcessAccept()
        {
            Interlocked.Increment(ref currentConnCount);
            T token = pool.Pop();
            lock (tokenList)
            {
                tokenList.Add(token);
            }
            token.Init(saea.AcceptSocket);
            token.OnTokenClose = OnTokenClosed;
            IOCPTool.ColorLog("Client Online, Allocate TokenID:{0}", LogColor.Green, token.tokenID);
            StartAccept();
        }

        private void OnTokenClosed(int tokenID)
        {
            int index = -1;
            for (int i = 0; i < tokenList.Count; i++)
            {
                if (tokenList[i].tokenID == tokenID)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                pool.Push(tokenList[index]);
                lock (tokenList)
                {
                    tokenList.RemoveAt(index);
                }
                Interlocked.Decrement(ref currentConnCount);
                acceptSemaphore.Release();
            }
            else
            {
                IOCPTool.Error("Token:{0} Cannot Find In Server TokenList.", tokenID);
            }
        }

        public void CloseServer()
        {
            for (int i = 0; i < tokenList.Count; i++)
            {
                tokenList[i].CloseToken();
            }
            tokenList = null;
            if (skt != null)
            {
                skt.Close();
                skt = null;
            }
        }

        public List<T> GetTokenList()
        {
            return tokenList;
        }
        #endregion

        #region Client
        public T token;

        public void StartAsClient(string ip, int port)
        {
            IPEndPoint pt = new IPEndPoint(IPAddress.Parse(ip), port);
            skt = new Socket(pt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            saea.RemoteEndPoint = pt;
            IOCPTool.ColorLog("Client Start...", LogColor.Green);
            StartConnect();
        }

        private void StartConnect()
        {
            bool suspend = skt.ConnectAsync(saea);
            // 连接建立成功
            if (!suspend)
            {
                ProcessConnect();
            }
            // 异步事件挂起，连接没有建立成功
            else
            {
            }
        }

        private void ProcessConnect()
        {
            token = new T();
            token.Init(skt);
        }

        public void CloseClient()
        {
            if (token != null)
            {
                token.CloseToken();
                token = null;
            }
            if (skt != null)
            {
                skt = null;
            }
        }
        #endregion

        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            switch (saea.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect();
                    break;
                case SocketAsyncOperation.Accept:
                    ProcessAccept();
                    break;
                default:
                    IOCPTool.Warn("The Last Operation Completed On The Socket Was Not Connect Or Accept.");
                    break;
            }
        }
    }
}

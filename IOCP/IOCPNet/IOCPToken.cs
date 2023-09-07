using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace PENet
{
    /// <summary>
    /// IOCP连接会话Token
    /// </summary>
    public abstract class IOCPToken<T> where T : IOCPMsg, new()
    {
        public int tokenID;
        private SocketAsyncEventArgs receiveSAEA;
        private SocketAsyncEventArgs sendSAEA;

        private Socket skt;
        private List<byte> readList = new List<byte>();
        /// <summary>
        /// 发送队列缓存
        /// </summary>
        private Queue<byte[]> cacheQueue = new Queue<byte[]>();
        /// <summary>
        /// 发送状态
        /// </summary>
        private bool isWrite = false;
        public Action<int> OnTokenClose;
        public TokenState tokenState = TokenState.None;

        public IOCPToken()
        {
            receiveSAEA = new SocketAsyncEventArgs();
            receiveSAEA.Completed += new System.EventHandler<SocketAsyncEventArgs>(IO_Completed);
            receiveSAEA.SetBuffer(new byte[2048], 0, 2048);

            sendSAEA = new SocketAsyncEventArgs();
            sendSAEA.Completed += new System.EventHandler<SocketAsyncEventArgs>(IO_Completed);
        }

        public void Init(Socket skt)
        {
            this.skt = skt;
            tokenState = TokenState.Connected;
            OnConnected();
            StartAsyncReceive();
        }

        #region 处理接收
        private void StartAsyncReceive()
        {
            bool suspend = skt.ReceiveAsync(receiveSAEA);
            if (!suspend)
            {
                ProcessReceive();
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        private void ProcessReceive()
        {
            if (receiveSAEA.BytesTransferred > 0 && receiveSAEA.SocketError == SocketError.Success)
            {
                byte[] bytes = new byte[receiveSAEA.BytesTransferred];
                Buffer.BlockCopy(receiveSAEA.Buffer, 0, bytes, 0, receiveSAEA.BytesTransferred);
                readList.AddRange(bytes);
                ProcessByteList();
                // 开始下一次接收
                StartAsyncReceive();
            }
            else
            {
                IOCPTool.Warn("Token:{0} Close:{1}", tokenID, receiveSAEA.SocketError.ToString());
                CloseToken();
            }
        }

        /// <summary>
        /// 处理接收到的数据
        /// </summary>
        private void ProcessByteList()
        {
            byte[] buff = IOCPTool.SplitLogicBytes(ref readList);
            if (buff != null)
            {
                T msg = IOCPTool.Deserialize<T>(buff);
                OnReceive(msg);
                ProcessByteList();
            }
        }
        #endregion

        #region 处理发送
        public bool SendMsg(IOCPMsg msg)
        {
            byte[] bytes = IOCPTool.PackLenInfo(IOCPTool.Serialize(msg));
            return SendMsg(bytes);
        }

        public bool SendMsg(byte[] bytes)
        {
            if (tokenState != TokenState.Connected)
            {
                IOCPTool.Warn("Connect Is Break, Cannot Send Net Message.");
                return false;
            }
            if (isWrite)
            {
                cacheQueue.Enqueue(bytes);
                return true;
            }
            isWrite = true;
            sendSAEA.SetBuffer(bytes, 0, bytes.Length);
            bool suspend = skt.SendAsync(sendSAEA);
            if (!suspend)
            {
                ProcessSend();
            }
            return true;
        }

        private void ProcessSend()
        {
            if (sendSAEA.SocketError == SocketError.Success)
            {
                isWrite = false;
                if (cacheQueue.Count > 0)
                {
                    byte[] item = cacheQueue.Dequeue();
                    SendMsg(item);
                }
            }
            else
            {
                IOCPTool.Error("Process Send Error:{0}", sendSAEA.SocketError.ToString());
                CloseToken();
            }
        }
        #endregion

        public void CloseToken()
        {
            if (skt != null)
            {
                tokenState = TokenState.Disconnected;
                OnDisconnected();

                OnTokenClose?.Invoke(tokenID);

                readList.Clear();
                cacheQueue.Clear();
                isWrite = false;

                try
                {
                    skt.Shutdown(SocketShutdown.Send);
                }
                catch (Exception e)
                {
                    IOCPTool.Error("Shutdown Socket Error:{0}", e.ToString());
                }
                finally
                {
                    skt.Close();
                    skt = null;
                }
            }
        }

        private void IO_Completed(object sender, SocketAsyncEventArgs saea)
        {
            switch (saea.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive();
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend();
                    break;
                default:
                    IOCPTool.Warn("The Last Operation Completed On The Socket Was Not Receive Or Send.");
                    break;
            }
        }

        protected abstract void OnConnected();
        protected abstract void OnDisconnected();
        protected abstract void OnReceive(T msg);
    }

    public enum TokenState
    {
        None,
        Connected,
        Disconnected
    }
}

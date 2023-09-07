using PENet;
using System;

namespace IOCPProtocol
{
    /// <summary>
    /// 网络数据协议消息体
    /// </summary>
    [Serializable]
    public class NetMsg : IOCPMsg
    {
        public string msg;
    }
}

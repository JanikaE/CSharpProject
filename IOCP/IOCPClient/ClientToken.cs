using IOCPProtocol;
using PENet;

namespace IOCPClientExample
{
    /// <summary>
    /// 客户端Token会话管理
    /// </summary>
    public class ClientToken : IOCPToken<NetMsg>
    {
        protected override void OnConnected()
        {
            IOCPTool.ColorLog("Connect To Server.", LogColor.Green);
        }

        protected override void OnDisconnected()
        {
            IOCPTool.ColorLog("Disconnect To Server.", LogColor.Yellow);
        }

        protected override void OnReceive(NetMsg msg)
        {
            IOCPTool.Log($"Receive Msg:{msg.msg}");
        }
    }
}

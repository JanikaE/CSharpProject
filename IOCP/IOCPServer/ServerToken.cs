using IOCPProtocol;
using PENet;

namespace IOCPServerExample
{
    /// <summary>
    /// 服务端Token回话管理
    /// </summary>
    public class ServerToken : IOCPToken<NetMsg>
    {
        protected override void OnConnected()
        {
            IOCPTool.ColorLog("Client Online.", LogColor.Green);
        }

        protected override void OnDisconnected()
        {
            IOCPTool.Warn("Client Offline.");
        }

        protected override void OnReceive(NetMsg msg)
        {
            IOCPTool.Log($"Receive Msg:{msg.msg}");
        }
    }
}

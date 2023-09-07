using IOCPProtocol;
using PENet;
using UnityEngine;

/// <summary>
/// UnityIOCP客户端入口
/// </summary>
public class GameStart : MonoBehaviour
{
    IOCPNet<UnityToken, NetMsg> client;

    void Start()
    {
        client = new IOCPNet<UnityToken, NetMsg>();
        client.StartAsClient("127.0.0.1", 6666);

        IOCPTool.LogFunc = Debug.Log;
        IOCPTool.ErrorFunc = Debug.LogError;
        IOCPTool.WarnFunc = Debug.LogWarning;
        IOCPTool.ColorLogFunc = (msg, color) =>
        {
            Debug.Log(color.ToString() + ":" + msg);
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NetMsg msg = new()
            { 
                msg = "Msg From Unity."
            };
            client.token.SendMsg(msg);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            client.CloseClient();
        }
    }
}

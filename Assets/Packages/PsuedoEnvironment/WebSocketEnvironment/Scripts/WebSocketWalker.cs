using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketWalker : RandomWalker {

	protected override void SendPosition(Walker walker)
    {
        if (WebSocketManager.Instance.SendPacket)
        {
            WebSocketManager.Instance.GetWebSocket.Send(JsonUtility.ToJson(walker));
        }
    }
}

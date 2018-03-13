using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebSocketWalker : RandomWalker {

	protected override void SendPosition(Vector2 position)
    {
        if (WebSocketManager.Instance.SendPacket)
        {
            WebSocketManager.Instance.GetWebSocket.Send($"{{ 'name' : {_name}, 'position' : {{ 'x' : {position.x}, 'y' : {position.y} }} }}");
        }
    }
}

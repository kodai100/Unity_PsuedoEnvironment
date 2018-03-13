using UnityEngine;
using System.Collections;

public class OSCWalker : RandomWalker {
    
    protected override void SendPosition(Vector2 position)
    {
        if (OSCManager.Instance.SendOSC)
        {
            OSCHandler.Instance.SendMessageToClient("Client", "/human" + _name + "/position/x", position.x);
            OSCHandler.Instance.SendMessageToClient("Client", "/human" + _name + "/position/y", position.y);
        }

    }
    
}

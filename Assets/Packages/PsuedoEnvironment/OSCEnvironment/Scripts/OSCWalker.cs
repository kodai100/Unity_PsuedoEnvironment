using UnityEngine;
using System.Collections;

public class OSCWalker : RandomWalker {
    
    protected override void SendPosition(Walker walker)
    {
        if (OSCManager.Instance.SendOSC)
        {
            OSCHandler.Instance.SendMessageToClient("Client", "/human" + walker.name + "/position/x", walker.position.x);
            OSCHandler.Instance.SendMessageToClient("Client", "/human" + walker.name + "/position/y", walker.position.y);
        }

    }
    
}

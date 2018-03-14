using UnityEngine;
using System.Collections;

public class OSCWalker : RandomWalker {
    private const string ClientStr = "Client";

    float lastSendTime = 0;
    float waitTime = 1f;  // ms

    protected override void SendPosition(Walker walker)
    {
        if (OSCManager.Instance.SendOSC)
        {
            OSCHandler.Instance.SendMessageToClient(ClientStr, walker.oscNameX, walker.position.x);
            OSCHandler.Instance.SendMessageToClient(ClientStr, walker.oscNameY, walker.position.y);

            lastSendTime = Time.realtimeSinceStartup;
        }

    }
    
}

using UnityEngine;
using System.Collections;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using PrefsGUI;

/// <summary>
/// This script must be atatched to content application
/// </summary>

public class Server: SingletonMonoBehaviour<Server>
{

    WebSocketServer server;

    [SerializeField] private PrefsInt _port = new PrefsInt("Open Port", 3000);

    void Start()
    {
        Reset();
    }

    void Reset()
    {
        server = new WebSocketServer(_port);

        server.AddWebSocketService<ProcessMessage>("/");
        server.Start();
    }

    public void DebugMenuGUI()
    {
        _port.OnGUI();

        if(GUILayout.Button("Recreate Server"))
        {
            server.Stop();
            server = null;
            Reset();
        }
    }

    void OnDestroy()
    {
        server.Stop();
        server = null;
    }

}

public class ProcessMessage : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {

        // Debug.Log(e.Data);
        // 受け取ったデータを利用する

    }
}

public class Echo : WebSocketBehavior
{
    protected override void OnMessage(MessageEventArgs e)
    {
        Sessions.Broadcast(e.Data);
    }

}
using System.Collections;
using UnityEngine;
using PrefsGUI;

using WebSocketSharp;

public class WebSocketManager : SingletonMonoBehaviour<WebSocketManager>
{
    public PrefsBool SendPacket = new PrefsBool("Send Signal", true);
    public PrefsString _ip = new PrefsString("IP", "127.0.0.1");
    public PrefsInt _port = new PrefsInt("Port", 3000);

    public WebSocket ws;

    public WebSocket GetWebSocket { get { return ws; } }

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        
        ws = new WebSocket("ws://" + _ip + ":" + _port.Get() + "/");
        

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("WebSocket Open");
        };

        ws.OnMessage += (sender, e) =>
        {
            // Debug.Log("WebSocket Message Type: " + e.GetType() + ", Data: " + e.Data);
        };

        ws.OnError += (sender, e) =>
        {
            Debug.Log("WebSocket Error Message: " + e.Message);
        };

        ws.OnClose += (sender, e) =>
        {
            Debug.Log("WebSocket Close");
        };

        ws.Connect();
    }

    public void DebugMenuGUI()
    {
        SendPacket.OnGUI();
        _ip.OnGUI();
        _port.OnGUI();

        if (GUILayout.Button("Reset WebSocket"))
        {
            ws.Close();
            ws = null;

            Reset();
        }
    }

    void Update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrefsGUI;
using UnityOSC;
using System.Net;

public class OSCManager : SingletonMonoBehaviour<OSCManager> {

    public PrefsBool SendOSC = new PrefsBool("Send Signal", true);
    public PrefsString _ip = new PrefsString("IP", "localhost");
    public PrefsInt _port = new PrefsInt("Port", 7100);
    public PrefsFloat OscWaitSec = new PrefsFloat("oscWaitSec", 0.03f);
    
    void Start () {
        OSCHandler.Instance.Init();

        OSCHandler.Instance.CreateClient("Client", IPAddress.Parse(_ip), _port);
	}

    public void DebugMenuGUI()
    {
        SendOSC.OnGUI();
        _ip.OnGUI();
        _port.OnGUI();
    }
	
	void Update () {

    }
}

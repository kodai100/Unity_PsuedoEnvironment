

namespace WebSocketEnvironment
{
    public class WebSocketDebugMenu : DebugMenu
    {
        
        protected override void Start()
        {

            base.Start();

            // 本来であればこれはコンテンツ側 (or Webサーバ) にあるべし
            folds.Add("WebSocket Server", () =>
            {
                Server.Instance.DebugMenuGUI();
            });

            folds.Add("WebSocket Client", () =>
            {

                WebSocketManager.Instance.DebugMenuGUI();

            });

            folds.Add("Manager", () =>
            {
                EnvironmentManager.Instance.DebugMenuGUI();

            });
        }

        

    }
}
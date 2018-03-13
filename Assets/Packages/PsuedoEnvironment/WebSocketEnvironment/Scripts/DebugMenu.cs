using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace WebSocketEnvironment
{
    public class DebugMenu : SingletonMonoBehaviour<DebugMenu>
    {

        protected Rect _windowRect;
        public bool _enable = false;

        [SerializeField] private int width = 700;
        [SerializeField] private int height = 500;

        protected GUIUtil.Folds folds = new GUIUtil.Folds();
        public List<GameObject> disableOnMenu;
        public List<GameObject> enableOnMenu;

        protected virtual void Start()
        {

            _windowRect = new Rect(20f, 20f, width, height);

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

        protected virtual void Update()
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                _enable = !_enable;

                if (!_enable)
                {
                    // ConfigBase.Instance.Save();
                    PrefsGUI.Prefs.Save();
                }

                disableOnMenu.ForEach(o => o.SetActive(!_enable));
                enableOnMenu.ForEach(o => o.SetActive(_enable));
            }
        }



        protected virtual void OnGUI()
        {
            if (_enable)
            {
                if (disableOnMenu.Any())
                {
                    GUILayout.Label("<color=red>Disable On Menu: " + string.Join(",", disableOnMenu.Select(obj => obj.name).ToArray()) + "</color>");
                }
                if (enableOnMenu.Any())
                {
                    GUILayout.Label("<color=green>Enable On Menu: " + string.Join(",", enableOnMenu.Select(obj => obj.name).ToArray()) + "</color>");
                }

                _windowRect = GUILayout.Window(0, _windowRect, (i) =>
                {
                    folds.OnGUI();
                    GUI.DragWindow();
                }, "DebugMenu");
            }
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace OSCEnvironment
{
    public class OSCDebugMenu : DebugMenu
    {

        protected override void Start()
        {

            base.Start();

            folds.Add("OSC", () =>
            {
                OSCManager.Instance.DebugMenuGUI();
            });

            folds.Add("Manager", () =>
            {
                EnvironmentManager.Instance.DebugMenuGUI();
            });
        }
    }
}
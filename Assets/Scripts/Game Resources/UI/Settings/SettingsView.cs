using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WitchDoctor.CoreResources.UIViews.BaseScripts;

namespace WitchDoctor.GameResources.UI.Settings
{
    public class SettingsView : UIView<SettingsView>
    {
        public Button ContinueButton;
        public Button QuitButton;

        public override void InitializeViewElements()
        {

        }

        public override void DeInitializeViewElements()
        {
            ContinueButton.onClick.RemoveAllListeners();
            QuitButton.onClick.RemoveAllListeners();
        }
    }
}

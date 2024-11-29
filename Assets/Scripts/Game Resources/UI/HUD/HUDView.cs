using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WitchDoctor.CoreResources.UIViews.BaseScripts;

namespace WitchDoctor.GameResources.UI.HUD
{
    public class HUDView : UIView<HUDView>
    {
        public Slider HealthSlider;
        public Slider ManaSlider;

        public override void InitializeViewElements()
        {

        }

        public override void DeInitializeViewElements()
        {

        }
    }
}
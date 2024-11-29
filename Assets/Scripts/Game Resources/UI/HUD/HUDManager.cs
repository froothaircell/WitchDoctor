using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchDoctor.CoreResources.UIViews.BaseScripts;

namespace WitchDoctor.GameResources.UI.HUD
{
    public class HUDManager : UIViewManager<HUDManager, HUDView>
    {
        #region Overrides
        protected override void InitializeManager()
        {
            base.InitializeManager();
        }

        protected override void DeInitializeManager()
        {
            base.DeInitializeManager();
        }

        public override void OnShowPanel()
        {
            GameConstants.OnPlayerHealthSet += SetPlayerHealth;

        }

        public override void OnHidePanel()
        {

        }
        #endregion

        #region Public Methods
        public void SetPlayerHealth(int health)
        {
            var sliderValClamp = (float) Mathf.Clamp(health, 0, GameConstants.PLAYER_MAX_HEALTH) / GameConstants.PLAYER_MAX_HEALTH;

            view.HealthSlider.SetValueWithoutNotify(sliderValClamp);
        }

        public void SetPlayerMana(int mana)
        {
            var sliderValClamp = (float) Mathf.Clamp(mana, 0, 100) / GameConstants.PLAYER_MAX_HEALTH;

            view.ManaSlider.SetValueWithoutNotify(sliderValClamp);
        }
        #endregion
    }
}

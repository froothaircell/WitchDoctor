using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchDoctor.CoreResources.UIViews.BaseScripts;
using WitchDoctor.Managers.InputManagement;

namespace WitchDoctor.GameResources.UI.HUD
{
    public class HUDManager : UIViewManager<HUDManager, HUDView>
    {
        #region Overrides
        protected override void InitializeManager()
        {
            base.InitializeManager();
            GameConstants.OnPlayerHealthSet += SetPlayerHealth;
        }

        protected override void DeInitializeManager()
        {
            if (!AppHandler.Instance.ApplicationQuitting)
                GameConstants.OnPlayerHealthSet -= SetPlayerHealth;

            base.DeInitializeManager();
        }

        public override void OnShowPanel()
        {
            InputManager.Player.Menu.performed += OnShowPauseMenu;
        }


        public override void OnHidePanel()
        {
            if (!AppHandler.Instance.ApplicationQuitting)
                InputManager.Player.Menu.performed -= OnShowPauseMenu;
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

        #region Private Methods
        private void TogglePause()
        {
            UIMediator.Instance.ToggleMenuVisibility(UIViewType.Settings, false);
        }
        #endregion

        #region Event Listeners
        private void OnShowPauseMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                TogglePause();
            }
        }
        #endregion
    }
}

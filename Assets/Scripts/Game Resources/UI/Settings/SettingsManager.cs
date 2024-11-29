using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WitchDoctor.CoreResources.UIViews.BaseScripts;
using WitchDoctor.Managers.InputManagement;

namespace WitchDoctor.GameResources.UI.Settings
{
    public class SettingsManager : UIViewManager<SettingsManager, SettingsView>
    {
        private UIViewType _precedingViewType = UIViewType.None;

        #region Overrides
        protected override void InitializeManager()
        {
            base.InitializeManager();
            _precedingViewType = UIViewType.None;

            view.ContinueButton.onClick.AddListener(HidePanel);
            view.QuitButton.onClick.AddListener(OnQuitGame);
        }

        protected override void DeInitializeManager()
        {
            _precedingViewType = UIViewType.None;
            base.DeInitializeManager();
        }

        public override void OnShowPanel()
        {
            base.OnShowPanel();
            
            if (UIMediator.Instance.CheckMenuVisibility(UIViewType.HUDMenu))
            {
                _precedingViewType = UIViewType.HUDMenu;
                UIMediator.Instance.SetMenuInteractability(_precedingViewType, false);

                // InputManager.Player.Back.performed += OnCloseSettings;

                AppHandler.Instance.FreezeApplication(true);
            }
        }

        public override void OnHidePanel()
        {
            if (!AppHandler.Instance.ApplicationQuitting)
            {
                // InputManager.Player.Back.performed -= OnCloseSettings;
            }

            if (AppHandler.Instance.ApplicationFrozen)
            {
                AppHandler.Instance.FreezeApplication(false);
            }

            UIMediator.Instance.SetMenuInteractability(_precedingViewType, true);

            base.OnHidePanel();
        }
        #endregion

        #region Event Listeners
        private void OnCloseSettings(InputAction.CallbackContext context)
        {
            if (context.performed)
                HidePanel();
        }

        private void OnQuitGame()
        {
            HidePanel();
            GameStateMediator.Instance.QuitGame();
        }
        #endregion
    }
}

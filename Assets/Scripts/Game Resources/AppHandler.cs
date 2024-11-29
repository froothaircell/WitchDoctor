using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchDoctor.CoreResources.Utils.Singleton;
using WitchDoctor.GameResources.CharacterScripts.Player;

namespace WitchDoctor.GameResources
{
    public class AppHandler : MonoSingleton<AppHandler>
    {
        [SerializeField]
        private GameStateMediator _mediator;
        [SerializeField]
        private bool _loadTestLevel = false;

        public bool ApplicationQuitting { get; private set; }
        public bool ApplicationFrozen { get; private set; }

        public bool LoadTestLevel => _loadTestLevel;

        #region Overrides
        public override void InitSingleton()
        {
            base.InitSingleton();

            _mediator.gameObject.SetActive(true);
            GameConstants.OnAppQuit += QuitGame;

            Debug.Log("App Handler Initialized");
        }

        public override void CleanSingleton()
        {
            if (!ApplicationQuitting)
                GameConstants.OnAppQuit -= QuitGame;

            base.CleanSingleton();
        }
        #endregion

        #region Public Methods
        public void QuitGame()
        {
            Debug.Log("Quitting Game");
            _mediator.gameObject.SetActive(false);

            Application.Quit();
        }

        public void FreezeApplication(bool status)
        {
            ApplicationFrozen = status;
            Time.timeScale = status ? 0 : 1;
        }
        #endregion
    }
}
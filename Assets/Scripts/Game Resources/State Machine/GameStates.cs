using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using WitchDoctor.CoreResources.StateMachine;
using WitchDoctor.CoreResources.UIViews.BaseScripts;
using WitchDoctor.Utils;

namespace WitchDoctor.GameResources.StateMachine
{
    public class GameState : StateHistory<GameStateMachine, GameState>
    {
    
    }

    public class GameState_Entry : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class GameState_Menu : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();

            SceneManager.LoadScene(1);

            var toggleMenus = !(PrevState != null && PrevState.GetType().Equals(typeof(GameState_Level1)));
            UIMediator.Instance.SetMenuVisibility(UIViewType.MainMenu, true, toggleMenus);
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }

    public class GameState_Level1 : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();

            AsyncOperation sceneLoadingOp;

            if (AppHandler.Instance.LoadTestLevel)
                sceneLoadingOp = SceneManager.LoadSceneAsync(2);
            else
                sceneLoadingOp = SceneManager.LoadSceneAsync(3);

            UIMediator.Instance.SetMenuVisibility(UIViewType.HUDMenu, true);
            GameConstants.OnLevelLoadStart?.Invoke(sceneLoadingOp);
        }

        public override void OnExit()
        {
            UIMediator.Instance.SetMenuVisibility(UIViewType.HUDMenu, false);

            AsyncOperation sceneLoadingOp;

            if (AppHandler.Instance.LoadTestLevel)
                sceneLoadingOp = SceneManager.UnloadSceneAsync(2);
            else
                sceneLoadingOp = SceneManager.UnloadSceneAsync(3);

            // GameConstants.OnLevelLoadStart?.Invoke(sceneLoadingOp);

            base.OnExit();
        }
    }

    public class GameState_Exit : GameState
    {
        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}
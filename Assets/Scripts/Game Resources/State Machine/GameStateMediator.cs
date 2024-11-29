using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchDoctor.CoreResources.Managers.CameraManagement;
using WitchDoctor.CoreResources.Managers.GeneralUtils;
using WitchDoctor.CoreResources.UIViews.BaseScripts;
using WitchDoctor.CoreResources.Utils.Singleton;
using WitchDoctor.GameResources;
using WitchDoctor.GameResources.StateMachine;
using WitchDoctor.Managers.InputManagement;

public class GameStateMediator : DestroyableMonoSingleton<GameStateMediator>
{
    private GameStateMachine _fsm;

    public GameState CurrentState => _fsm.CurrentState;

    public bool SystemsInstantiated => IsInstantiated && 
        CameraManager.IsInstantiated && InputManager.IsInstantiated && 
        UIMediator.IsInstantiated && SoundManager.IsInstantiated;

    [Space(5)]
    
    [Header("Mediators")]
    [SerializeField]
    private CameraManager _cameraManager;
    [SerializeField]
    private InputManager _inputManager;
    [SerializeField]
    private UIMediator _uIMediator;
    [SerializeField]
    private SoundManager _soundManager;

    #region Overrides
    public override void InitSingleton()
    {
        base.InitSingleton();

        _cameraManager.gameObject.SetActive(true);
        _inputManager.gameObject.SetActive(true);
        _uIMediator.gameObject.SetActive(true);
        _soundManager.gameObject.SetActive(true);

        StartCoroutine(AwaitSystemInit(() =>
        {
            _fsm = new GameStateMachine();
            _fsm.GoToState<GameState_Menu>();

            GameConstants.OnLevelLoadStart += OnLevelLoadStart;
        }));
    }

    public override void CleanSingleton()
    {
        if (!AppHandler.Instance.ApplicationQuitting)
            GameConstants.OnLevelLoadStart -= OnLevelLoadStart;

        GameConstants.ResetActions();
        _cameraManager?.gameObject.SetActive(false);
        _inputManager?.gameObject.SetActive(false);
        _uIMediator?.gameObject.SetActive(false);
        _soundManager?.gameObject.SetActive(false);

        base.CleanSingleton();
    }
    #endregion

    #region Public Methods
    public void StartGame()
    {
        if (CurrentState.GetType().Equals(typeof(GameState_Menu)))
            _fsm.GoToState<GameState_Level1>();
    }


    #endregion

    #region Private Methods
    private IEnumerator AwaitSystemInit(Action OnComplete)
    {
        yield return new WaitUntil(() => SystemsInstantiated);

        OnComplete?.Invoke();
    }

    private IEnumerator LoadLevel(AsyncOperation op)
    {
        yield return new WaitUntil(() => op.isDone);

        UIMediator.Instance.SetMenuVisibility(UIViewType.Loading, false, false);
    }
    #endregion

    #region Event Listeners
    private void OnLevelLoadStart(AsyncOperation op)
    {
        UIMediator.Instance.SetMenuVisibility(UIViewType.Loading, true, false);

        StartCoroutine(LoadLevel(op));
    }
    #endregion
}

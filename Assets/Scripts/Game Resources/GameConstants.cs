using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WitchDoctor.GameResources
{
    public static class GameConstants
    {
        #region Constants
        public const int PLAYER_MAX_HEALTH = 150;
        public const int PLAYER_MAX_MANA = 100;
        #endregion

        #region Events
        public static Action OnAppQuit;
        public static Action<int> OnPlayerHealthSet;
        public static Action<float> OnPlayerManaSet;
        public static Action<AsyncOperation> OnLevelLoadStart;
        #endregion

        public static void ResetActions()
        {
            OnAppQuit = null;
            OnPlayerHealthSet = null;
            OnPlayerManaSet = null;
            OnLevelLoadStart = null;
        }
    }
}
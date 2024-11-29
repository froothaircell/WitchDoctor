using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WitchDoctor.CoreResources.UIViews.BaseScripts;

namespace WitchDoctor.GameResources.UI.Loading
{
    public class LoadingManager : UIViewManager<LoadingManager, LoadingView>
    {
        [SerializeField]
        private float _refreshTime = 0.7f;

        private int _numDots = 0;
        private Coroutine _loadingCoroutine;

        #region Overrides
        protected override void InitializeManager()
        {
            base.InitializeManager();
        }

        protected override void DeInitializeManager()
        {
            if (_loadingCoroutine != null)
            {
                StopCoroutine(_loadingCoroutine);
                _loadingCoroutine = null;
            }

            base.DeInitializeManager();
        }

        public override void OnShowPanel()
        {
            view.LoadingText.SetText("Loading");
            _numDots = 0;

            if (_loadingCoroutine != null)
            {
                StopCoroutine(_loadingCoroutine);
                _loadingCoroutine = null;
            }

            _loadingCoroutine = StartCoroutine(LoadScreenCoroutine());
        }

        public override void OnHidePanel()
        {
            if (_loadingCoroutine != null)
            {
                StopCoroutine(_loadingCoroutine);
                _loadingCoroutine = null;
            }

            view.LoadingText.SetText("Loading");
            _numDots = 0;
        }
        #endregion

        private IEnumerator LoadScreenCoroutine()
        {
            while (true)
            {
                _numDots = _numDots >= 3 ? 0 : _numDots + 1;
                string dots = new string('.', _numDots);
                view.LoadingText.SetText("Loading" + dots);

                yield return new WaitForSeconds(_refreshTime);
            }
        }
    }
}

using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class WinScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _splat;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.OnClickAsObservable().Subscribe(NextGame).AddTo(this);
            
            ShowWinScreen();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async void NextGame(Unit _)
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion();
            
            GameStateMachine.Enter<StateLoadProgress>();
        }
        
        private void ShowWinScreen()
        {
            _button.gameObject.SetActive(false);
            _splat.localScale = Vector3.zero;
            _splat
                .DOScale(Vector3.one * 1.5f, 1f)
                .SetEase(Ease.OutBack)
                .OnComplete(ShowButton);
        }

        private void ShowButton()
        {
            _button.gameObject.SetActive(true);
            _button.transform.PunchTransform();
        }
    }
}
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

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.OnClickAsObservable().First().Subscribe(NextGame).AddTo(this);
            
            ShowButton();
            
            FadeCanvas(0f, 1f, 0.25f);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async void NextGame(Unit _)
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion();
            
            GameStateService.Enter<StateLoadProgress>();
        }

        private void ShowButton()
        {
            _button.gameObject.SetActive(true);
            _button.transform.PunchTransform();
        }
    }
}
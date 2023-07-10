using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _text;

        protected override void OnEnable()
        {
            base.OnEnable();

            _button.OnClickAsObservable().First().Subscribe(StartGame).AddTo(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async void StartGame(Unit _)
        {
            _text.PunchTransform();
            
            await FadeCanvas(1f, 0f, 0.25f).AsyncWaitForCompletion();
            
            GameStateService.Enter<StateGame>();
        }
    }
}
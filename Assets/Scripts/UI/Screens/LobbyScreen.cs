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

            _button.OnClickAsObservable().Subscribe(StartGame).AddTo(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async void StartGame(Unit _)
        {
            await _text.PunchTransform().AsyncWaitForCompletion();
            
            GameStateService.Enter<StateGame>();
        }
    }
}
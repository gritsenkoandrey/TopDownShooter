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
            
            _button.OnClickAsObservable().Subscribe(NextGame).AddTo(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async void NextGame(Unit _)
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion();
            
            GameStateMachine.Enter<LoadProgressState>();
        }
    }
}
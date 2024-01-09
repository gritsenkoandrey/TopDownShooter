using CodeBase.App;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
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
            
            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => NextGame().Forget())
                .AddTo(this);

            ScreenAnimation().Forget();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async UniTaskVoid NextGame()
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();
            
            GameStateService.Enter<StatePreview, string>(SceneName.Lobby);
        }

        private async UniTaskVoid ScreenAnimation()
        {
            _button.gameObject.SetActive(false);

            await FadeCanvas(0f, 1f, 0.5f).AsyncWaitForCompletion().AsUniTask();
            
            _button.gameObject.SetActive(true);

            _button.transform.PunchTransform();
        }
    }
}
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private Button _button;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => NextState().Forget())
                .AddTo(LifeTimeDisposable);
        }

        private async UniTaskVoid NextState()
        {
            SetCanvasEnable(false);
            _button.transform.PunchTransform();
            await FadeCanvas(1f, 0f, 0.25f).AsyncWaitForCompletion().AsUniTask();
            CloseScreen.Execute();
        }
    }
}
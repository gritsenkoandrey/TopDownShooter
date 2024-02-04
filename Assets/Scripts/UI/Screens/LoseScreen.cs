using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LoseScreen : BaseScreen
    {
        [SerializeField] private Button _button;

        private Tween _tween;

        private protected override void OnEnable()
        {
            base.OnEnable();
            
            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => NextState().Forget())
                .AddTo(LifeTimeDisposable);
            
            ShowButton().Forget();
        }

        private protected override void OnDisable()
        {
            base.OnDisable();
            
            _tween?.Kill();
        }

        private async UniTaskVoid NextState()
        {
            _tween?.Kill();
            
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();

            ChangeState.Execute();
        }

        private async UniTaskVoid ShowButton()
        {
            _tween = FadeCanvas(0f, 1f, 0.5f);
            
            await _tween.AsyncWaitForCompletion().AsUniTask();
            
            _tween = BounceButton(_button, 1.05f, 0.5f);
        }
    }
}
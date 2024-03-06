using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class SettingsScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        
        private Tween _tween;

        private protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => Close().Forget())
                .AddTo(LifeTimeDisposable);
            
            ShowButton().Forget();
        }
        
        private protected override void OnDisable()
        {
            base.OnDisable();
            
            _tween?.Kill();
        }

        private async UniTaskVoid Close()
        {
            _button.transform.PunchTransform();
            await FadeCanvas(1f, 0f, 0.25f).AsyncWaitForCompletion().AsUniTask();
            CloseScreen.Execute();
        }
        
        private async UniTaskVoid ShowButton()
        {
            ActivateButton(_button, false);
            _tween = FadeCanvas(0f, 1f, 0.5f);
            await _tween.AsyncWaitForCompletion().AsUniTask();
            ActivateButton(_button, true);
            _tween = BounceButton(_button, 1.05f, 0.5f);
        }
    }
}
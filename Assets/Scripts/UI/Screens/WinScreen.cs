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
        
        private Tween _showButton;

        private protected override void OnEnable()
        {
            base.OnEnable();
            
            _button
                .OnClickAsObservable()
                .DoOnSubscribe(AwaitScreenAnimation)
                .First()
                .Subscribe(_ => NextGame().Forget())
                .AddTo(this);

            FadeCanvas(0f, 1f, 0.25f);
        }

        private protected override void OnDisable()
        {
            base.OnDisable();
            
            _showButton?.Kill();
        }

        private async UniTaskVoid NextGame()
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();

            ChangeState.Execute();
        }
        
        private void AwaitScreenAnimation()
        {
            _button.gameObject.SetActive(false);

            _showButton = DOVirtual.DelayedCall(2f, ShowButton);
        }

        private void ShowButton()
        {
            _button.gameObject.SetActive(true);
            _button.transform.PunchTransform();
        }
    }
}
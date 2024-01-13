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
        [SerializeField] private Transform _splat;

        private protected override void OnEnable()
        {
            base.OnEnable();
            
            _button
                .OnClickAsObservable()
                .DoOnSubscribe(ScreenAnimation)
                .First()
                .Subscribe(_ => RestartGame().Forget())
                .AddTo(this);

            FadeCanvas(0f, 1f, 0.25f);
        }

        private async UniTaskVoid RestartGame()
        {
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();

            ChangeState.Execute();
        }

        private void ScreenAnimation()
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
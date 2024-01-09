using CodeBase.App;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class PreviewScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private CCharacterPreviewMediator _characterPreviewMediator;

        private Tween _bounceTween;

        protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => StartGame().Forget())
                .AddTo(this);
            
            FadeCanvas(0f, 1f, 0.2f);
            
            BounceButton();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            Dispose();
        }

        private async UniTaskVoid StartGame()
        {
            _characterPreviewMediator.SelectCharacter.Execute();
            
            Dispose();
            
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();

            await UniTask.WaitUntil(() => _characterPreviewMediator.CharacterPreviewAnimator.IsExitAnimation);
            
            GameStateService.Enter<StateLoadLevel, string>(SceneName.Main);
        }

        private void BounceButton()
        {
            _bounceTween = _button.transform
                .DOScale(Vector3.one * 1.05f, 0.5f)
                .SetEase(Ease.InOutQuad)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void Dispose()
        {
            _bounceTween?.Kill();
        }
    }
}
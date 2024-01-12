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
                .DoOnSubscribe(ScreenAnimation)
                .First()
                .Subscribe(_ => StartGame().Forget())
                .AddTo(this);
            
            FadeCanvas(0f, 1f, 0.2f);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private async UniTaskVoid StartGame()
        {
            _characterPreviewMediator.SelectCharacter.Execute();
            
            _bounceTween?.Kill();
            
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();

            await UniTask.WaitUntil(() => _characterPreviewMediator.CharacterPreviewAnimator.IsExitAnimation);
            
            GameStateService.Enter<StateLoadLevel, string>(SceneName.Main);
        }

        private void ScreenAnimation()
        {
            _bounceTween = BounceButton(_button, 1.05f, 0.5f);
        }
    }
}
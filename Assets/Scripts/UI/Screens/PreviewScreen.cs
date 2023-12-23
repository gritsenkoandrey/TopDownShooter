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
        [SerializeField] private CCharacterPreviewMediator _cCharacterPreviewMediator;

        protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
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
            _cCharacterPreviewMediator.SelectCharacter.Execute();
            
            await _button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();

            await UniTask.WaitUntil(() => _cCharacterPreviewMediator.CharacterPreviewAnimator.IsExitAnimation);
            
            GameStateService.Enter<StateLoadLevel, string>(SceneName.Main);
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using CodeBase.UI.Screens;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SSettingsButton : SystemComponent<CSettingsButton>
    {
        private IUIFactory _uiFactory;
        private PauseModel _pauseModel;

        [Inject]
        private void Construct(IUIFactory uiFactory, PauseModel pauseModel)
        {
            _uiFactory = uiFactory;
            _pauseModel = pauseModel;
        }
        
        protected override void OnEnableComponent(CSettingsButton component)
        {
            base.OnEnableComponent(component);

            component.Button
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    Entities.Foreach(button => button.Button.interactable = false);

                    component.Tween = component.Button.transform
                        .DOLocalRotate(Vector3.back * 180f, 0.5f)
                        .SetRelative()
                        .OnComplete(CreatePopUp);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CSettingsButton component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private async UniTaskVoid CreateSettingsPopUp()
        {
            BaseScreen screen = await _uiFactory.CreatePopUp(ScreenType.Settings);

            _pauseModel.OnPause.Execute(true);

            screen.CloseScreen
                .First()
                .Subscribe(CloseScreen)
                .AddTo(LifetimeDisposable);
        }

        private void CreatePopUp() => CreateSettingsPopUp().Forget();

        private void CloseScreen(Unit _)
        {
            _pauseModel.OnPause.Execute(false);
            
            Entities.Foreach(button => button.Button.interactable = true);
        }
    }
}
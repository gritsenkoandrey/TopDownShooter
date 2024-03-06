using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Models;
using CodeBase.UI.Screens;
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
        private IGuiService _guiService;
        private PauseModel _pauseModel;

        private const float Duration = 0.5f;
        private const float Rotate = 180f;

        [Inject]
        private void Construct(IUIFactory uiFactory, IGuiService guiService, PauseModel pauseModel)
        {
            _uiFactory = uiFactory;
            _guiService = guiService;
            _pauseModel = pauseModel;
        }
        
        protected override void OnEnableComponent(CSettingsButton component)
        {
            base.OnEnableComponent(component);

            component.Button
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(Duration * 2))
                .Subscribe(_ =>
                {
                    component.Tween = component.Image
                        .DOLocalRotate(Vector3.back * Rotate, Duration)
                        .SetRelative()
                        .OnComplete(CreateScreen);
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CSettingsButton component)
        {
            base.OnDisableComponent(component);
            
            component.Tween?.Kill();
        }

        private async UniTaskVoid CreateSettingsScreen()
        {
            BaseScreen screen = await _uiFactory.CreatePopUp(ScreenType.Settings);

            _pauseModel.OnPause.Execute(true);

            screen.CloseScreen
                .First()
                .Subscribe(CloseScreen)
                .AddTo(LifetimeDisposable);
        }

        private void CreateScreen() => CreateSettingsScreen().Forget();

        private void CloseScreen(Unit _)
        {
            _pauseModel.OnPause.Execute(false);
            
            _guiService.Pop();
        }
    }
}
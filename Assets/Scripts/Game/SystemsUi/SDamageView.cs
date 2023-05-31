using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.GUI;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SDamageView : SystemComponent<CDamageView>
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;

        public SDamageView(IGameFactory gameFactory, ICameraService cameraService, IGuiService guiService)
        {
            _gameFactory = gameFactory;
            _cameraService = cameraService;
            _guiService = guiService;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CDamageView component)
        {
            base.OnEnableComponent(component);

            foreach (IEnemy enemy in _gameFactory.Character.Enemies)
            {
                enemy.DamageReceived
                    .Subscribe(damage =>
                    {
                        Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(enemy.Position);
                        
                        component.Sequence?.Kill();
                        component.Text.text = damage.ToString();
                        component.CanvasGroup.alpha = 1f;
                        component.transform.position = screenPoint;

                        StartAnimation(component);
                    })
                    .AddTo(component.LifetimeDisposable);
            }
        }

        protected override void OnDisableComponent(CDamageView component)
        {
            base.OnDisableComponent(component);
            
            component.Sequence?.Kill();
        }

        private void StartAnimation(CDamageView component)
        {
            component.Sequence = DOTween.Sequence()
                .Append(component.transform
                    .DOMoveY(350f * _guiService.StaticCanvas.Canvas.scaleFactor, 1f)
                    .SetRelative()
                    .SetEase(Ease.OutCirc))
                .Join(component.CanvasGroup
                    .DOFade(0f, 1f)
                    .SetEase(Ease.Linear));
        }
    }
}
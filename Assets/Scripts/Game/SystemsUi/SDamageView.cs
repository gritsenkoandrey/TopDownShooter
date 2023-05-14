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
                        component.Sequence?.Kill();
                        component.Text.text = damage.ToString();
                        component.CanvasGroup.alpha = 1f;

                        Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(enemy.Position);

                        component.transform.position = screenPoint;

                        component.Sequence = DOTween.Sequence();

                        component.Sequence.Append(component.transform
                            .DOMoveY(350f * _guiService.StaticCanvas.Canvas.scaleFactor, 1f)
                            .SetRelative()
                            .SetEase(Ease.Linear));

                        component.Sequence.Join(component.CanvasGroup
                            .DOFade(0f, 1f)
                            .SetEase(Ease.Linear));
                    })
                    .AddTo(component.LifetimeDisposable);
            }
        }

        protected override void OnDisableComponent(CDamageView component)
        {
            base.OnDisableComponent(component);
            
            component.Sequence?.Kill();
        }
    }
}
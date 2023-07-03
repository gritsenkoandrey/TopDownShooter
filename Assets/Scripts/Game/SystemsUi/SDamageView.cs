using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SDamageView : SystemComponent<CDamageView>
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICameraService _cameraService;

        public SDamageView(IGameFactory gameFactory, ICameraService cameraService)
        {
            _gameFactory = gameFactory;
            _cameraService = cameraService;
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
                        
                        component.Text.text = damage.ToString();
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
            component.Sequence?.Kill();

            component.Sequence = DOTween.Sequence()
                .Append(component.transform.DOMoveY(350f, 1f).SetRelative().SetEase(Ease.OutCirc))
                .Join(component.CanvasGroup.DOFade(0f, 1f).From(1f).SetEase(Ease.Linear));
        }
    }
}
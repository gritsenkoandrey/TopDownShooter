using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SDamageView : SystemComponent<CDamageView>
    {
        private readonly ICameraService _cameraService;
        private readonly LevelModel _levelModel;

        public SDamageView(ICameraService cameraService, LevelModel levelModel)
        {
            _cameraService = cameraService;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CDamageView component)
        {
            base.OnEnableComponent(component);
            
            _levelModel.Enemies.Foreach(enemy => SubscribeOnEnemyDamage(component, enemy));
        }

        protected override void OnDisableComponent(CDamageView component)
        {
            base.OnDisableComponent(component);
            
            component.Sequence?.Kill();
        }

        private void SubscribeOnEnemyDamage(CDamageView component, IEnemy enemy)
        {
            enemy.Health.CurrentHealth
                .Pairwise()
                .Where(health => health.Current < health.Previous)
                .Subscribe(health =>
                {
                    Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(enemy.Position);

                    component.Text.text = (health.Previous - health.Current).ToString();
                    component.transform.position = screenPoint;

                    StartAnimation(component);
                })
                .AddTo(component.LifetimeDisposable);
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
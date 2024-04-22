using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Utils;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealthUpdate : SystemComponent<CEnemyHealth>
    {
        private ICameraService _cameraService;

        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Entities.Foreach(UpdatePosition);
        }

        protected override void OnEnableComponent(CEnemyHealth component)
        {
            base.OnEnableComponent(component);

            component.Enemy
                .First(enemy => enemy != null)
                .Subscribe(enemy => SubscribeOnChangeHealth(component, enemy))
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnChangeHealth(CEnemyHealth component, IEnemy enemy)
        {
            enemy.Health.CurrentHealth
                .Subscribe(health =>
                {
                    float fillAmount = Mathematics.Remap(0, enemy.Health.MaxHealth, 0, 1, health);
                    
                    component.Text.text = enemy.Health.ToString();
                    component.Fill.fillAmount = fillAmount;
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void UpdatePosition(CEnemyHealth component)
        {
            if (component.Enemy.Value.Health.IsAlive == false)
            {
                component.CanvasGroup.alpha = 0f;
                
                return;
            }
            
            float height = component.Enemy.Value.Height;
            Vector3 position = component.Enemy.Value.Position.AddY(height);
            Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(position);
            Vector3 viewportPoint = _cameraService.Camera.WorldToViewportPoint(position);
            component.transform.position = screenPoint.ZeroZ();
            component.CanvasGroup.alpha += _cameraService.IsOnScreen(viewportPoint) 
                ? Time.deltaTime * 1f : Time.deltaTime * -1f;
        }
    }
}
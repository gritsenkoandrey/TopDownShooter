using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealthProvider : SystemComponent<CEnemyHealthProvider>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IUIFactory _uiFactory;
        private readonly ICameraService _cameraService;

        public SEnemyHealthProvider(IGameFactory gameFactory, IUIFactory uiFactory, ICameraService cameraService)
        {
            _gameFactory = gameFactory;
            _uiFactory = uiFactory;
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

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();

            foreach (CEnemyHealthProvider provider in Entities)
            {
                UpdatePosition(provider);
            }
        }

        protected override void OnEnableComponent(CEnemyHealthProvider component)
        {
            base.OnEnableComponent(component);

            CreateEnemyHealths(component);
        }

        protected override void OnDisableComponent(CEnemyHealthProvider component)
        {
            base.OnDisableComponent(component);
        }

        private void CreateEnemyHealths(CEnemyHealthProvider component)
        {
            foreach (IEnemy enemy in _gameFactory.Character.Enemies)
            {
                CEnemyHealth enemyHealth = _uiFactory.CreateEnemyHealth(enemy, component.transform);
                
                component.EnemyHealths.Add(enemyHealth);
            }
        }

        private void UpdatePosition(CEnemyHealthProvider component)
        {
            Plane[] frustumPlanes = GeometryUtility.CalculateFrustumPlanes(_cameraService.Camera);
            
            foreach (CEnemyHealth enemyHealth in component.EnemyHealths)
            {
                Vector3 enemyPosition = enemyHealth.Enemy.Value.Position.AddY(enemyHealth.Enemy.Value.Stats.Height);
                Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(enemyPosition);
                enemyHealth.transform.position = screenPoint;

                if (enemyHealth.Enemy.Value.Health.IsAlive)
                {
                    bool isVisible = GeometryUtility.TestPlanesAABB(frustumPlanes, enemyHealth.Enemy.Value.Health.Collider.bounds);

                    enemyHealth.CanvasGroup.alpha = isVisible ? 1f : 0f;
                }
            }
        }
    }
}
using System.Collections.Generic;
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
            component.EnemyHealths = new List<CEnemyHealth>(_gameFactory.Character.Enemies.Count);
            
            foreach (IEnemy enemy in _gameFactory.Character.Enemies)
            {
                CEnemyHealth enemyHealth = _uiFactory.CreateEnemyHealth(enemy, component.transform);
                
                component.EnemyHealths.Add(enemyHealth);
            }
        }

        private void UpdatePosition(CEnemyHealthProvider component)
        {
            for (int i = 0; i < component.EnemyHealths.Count; i++)
            {
                float height = component.EnemyHealths[i].Enemy.Value.Stats.Height;
                Vector3 position = component.EnemyHealths[i].Enemy.Value.Position.AddY(height);
                Vector3 screenPoint = _cameraService.Camera.WorldToScreenPoint(position);
                Vector3 viewportPoint = _cameraService.Camera.WorldToViewportPoint(position);
                component.EnemyHealths[i].transform.position = screenPoint.ZeroZ();

                if (component.EnemyHealths[i].Enemy.Value.Health.IsAlive)
                {
                    component.EnemyHealths[i].CanvasGroup.alpha = IsOnScreen(viewportPoint) ? 1f : 0f;
                }
            }
        }
        
        private bool IsOnScreen(Vector3 viewportPoint) => viewportPoint is { x: > 0f and < 1f, y: > 0f and < 1f };
    }
}
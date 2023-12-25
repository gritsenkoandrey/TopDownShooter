using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SEnemyHealthProvider : SystemComponent<CEnemyHealthProvider>
    {
        private readonly IUIFactory _uiFactory;
        private readonly ICameraService _cameraService;
        private readonly LevelModel _levelModel;

        public SEnemyHealthProvider(IUIFactory uiFactory, ICameraService cameraService, LevelModel levelModel)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _levelModel = levelModel;
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

            CreateEnemyHealths(component).Forget();
        }

        private async UniTaskVoid CreateEnemyHealths(CEnemyHealthProvider component)
        {
            component.EnemyHealths = new List<CEnemyHealth>(_levelModel.Enemies.Count);
            
            foreach (IEnemy enemy in _levelModel.Enemies)
            {
                CEnemyHealth enemyHealth = await _uiFactory.CreateEnemyHealth(enemy, component.transform);
                
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
using System;
using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SDamageViewProvider : SystemComponent<CDamageViewProvider>
    {
        private readonly IUIFactory _uiFactory;
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;
        private readonly LevelModel _levelModel;

        private float _scaleFactor;

        public SDamageViewProvider(IUIFactory uiFactory, ICameraService cameraService, IGuiService guiService, LevelModel levelModel)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _guiService = guiService;
            _levelModel = levelModel;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _scaleFactor = _guiService.StaticCanvas.Canvas.scaleFactor;
        }

        protected override void OnEnableComponent(CDamageViewProvider component)
        {
            base.OnEnableComponent(component);
            
            foreach (IEnemy enemy in _levelModel.Enemies)
            {
                SubscribeOnDamageEnemy(component, enemy);
            }
        }

        protected override void OnDisableComponent(CDamageViewProvider component)
        {
            base.OnDisableComponent(component);
            
            component.DamageViews = Array.Empty<CDamageView>(); 
        }

        private void SubscribeOnDamageEnemy(CDamageViewProvider component, IEnemy enemy)
        {
            enemy.Health.CurrentHealth
                .Pairwise()
                .Where(health => health.Current < health.Previous)
                .Subscribe(health =>
                {
                    ActivateDamageView(component, enemy, health.Previous - health.Current);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void ActivateDamageView(CDamageViewProvider component, IEnemy enemy, int damage)
        {
            if (component.DamageViews.Count == 0)
            {
                component.DamageViews = new List<CDamageView>();
            }
            else
            {
                for (int i = 0; i < component.DamageViews.Count; i++)
                {
                    if (component.DamageViews[i].Settings.IsActive)
                    {
                        continue;
                    }
                    
                    ActivateUpdateDamageView(component.DamageViews[i], enemy, damage);
                    
                    return;
                }
            }
            
            InstantiateDamageView(component, enemy, damage).Forget();
        }

        private async UniTaskVoid InstantiateDamageView(CDamageViewProvider component, IEnemy enemy, int damage)
        {
            CDamageView damageView = await _uiFactory.CreateDamageView(component.transform);
            ActivateUpdateDamageView(damageView, enemy, damage);
            component.DamageViews.Add(damageView);
        }

        private void ActivateUpdateDamageView(CDamageView component, IEnemy enemy, int damage)
        {
            Vector3 enemyPosition = enemy.Position.AddY(enemy.Stats.Height);
            Vector3 viewportPoint = _cameraService.Camera.WorldToViewportPoint(enemyPosition);
            float offset = _scaleFactor * component.Offset;
            float dirX = viewportPoint.x > 0.5f ? 1f : -1f;
            float dirY = viewportPoint.y > 0.5f ? 1f : -1f;
            
            Vector3 from = Vector3.zero;
            Vector3 to = new Vector3(dirX, dirY, 0f) * offset;
            Vector3 center = Vector3.Lerp(from, to, 0.25f).AddY(offset * dirY * 2f);
            
            component.Settings.From = from;
            component.Settings.Center = center;
            component.Settings.To = to;
            component.Settings.Index = 0;
            component.Settings.Target = enemy;
            component.Settings.IsActive = true;

            component.Text.text = damage.ToString();
        }
    }
}
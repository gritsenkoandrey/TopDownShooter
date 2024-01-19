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
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SDamageCombatLogViewProvider : SystemComponent<CDamageCombatLogViewProvider>
    {
        private readonly IUIFactory _uiFactory;
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;
        private readonly DamageCombatLog _damageCombatLog;

        private float _scaleFactor;
        private float _time;

        public SDamageCombatLogViewProvider(IUIFactory uiFactory, ICameraService cameraService, IGuiService guiService, 
            DamageCombatLog damageCombatLog)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _guiService = guiService;
            _damageCombatLog = damageCombatLog;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _scaleFactor = _guiService.StaticCanvas.Canvas.scaleFactor;
        }

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Entities.Foreach(UpdateDamageView);
        }

        protected override void OnDisableComponent(CDamageCombatLogViewProvider component)
        {
            base.OnDisableComponent(component);
            
            component.DamageCombatLogViews = Array.Empty<CDamageCombatLogView>();
        }

        private void UpdateDamageView(CDamageCombatLogViewProvider component)
        {
            if (_damageCombatLog.HasDamage() == false)
            {
                return;
            }
            
            _time += Time.deltaTime;

            if (_time > 0.1f)
            {
                _time = 0f;
                    
                ActivateDamageView(component);
            }
        }

        private void ActivateDamageView(CDamageCombatLogViewProvider component)
        {
            (IEnemy enemy, int damage) = _damageCombatLog.Dequeue();
            
            if (component.DamageCombatLogViews.Count == 0)
            {
                component.DamageCombatLogViews = new List<CDamageCombatLogView>();
            }
            else
            {
                for (int i = 0; i < component.DamageCombatLogViews.Count; i++)
                {
                    if (component.DamageCombatLogViews[i].Settings.IsActive)
                    {
                        continue;
                    }
                    
                    ActivateUpdateDamageView(component.DamageCombatLogViews[i], enemy, damage);
                    
                    return;
                }
            }
            
            InstantiateDamageView(component, enemy, damage).Forget();
        }

        private async UniTaskVoid InstantiateDamageView(CDamageCombatLogViewProvider component, IEnemy enemy, int damage)
        {
            CDamageCombatLogView damageCombatLogView = await _uiFactory.CreateDamageView(component.transform);
            ActivateUpdateDamageView(damageCombatLogView, enemy, damage);
            component.DamageCombatLogViews.Add(damageCombatLogView);
        }

        private void ActivateUpdateDamageView(CDamageCombatLogView component, IEnemy enemy, int damage)
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
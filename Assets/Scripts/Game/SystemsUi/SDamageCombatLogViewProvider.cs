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
    public sealed class SDamageCombatLogViewProvider : SystemComponent<CDamageCombatLogViewProvider>
    {
        private readonly IUIFactory _uiFactory;
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;
        private readonly DamageCombatLog _damageCombatLog;

        public SDamageCombatLogViewProvider(IUIFactory uiFactory, ICameraService cameraService, IGuiService guiService, 
            DamageCombatLog damageCombatLog)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _guiService = guiService;
            _damageCombatLog = damageCombatLog;
        }

        protected override void OnEnableComponent(CDamageCombatLogViewProvider component)
        {
            base.OnEnableComponent(component);

            _damageCombatLog.CombatLog
                .Subscribe(combatLog => ActivateDamageView(component, combatLog))
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CDamageCombatLogViewProvider component)
        {
            base.OnDisableComponent(component);
            
            component.DamageCombatLogViews = Array.Empty<CDamageCombatLogView>();
        }

        private void ActivateDamageView(CDamageCombatLogViewProvider component, CombatLog combatLog)
        {
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
                    
                    InitializeDamageView(component.DamageCombatLogViews[i], combatLog.Target, combatLog.Damage);
                    
                    return;
                }
            }
            
            InstantiateDamageView(component, combatLog.Target, combatLog.Damage).Forget();
        }

        private async UniTaskVoid InstantiateDamageView(CDamageCombatLogViewProvider component, ITarget target, int damage)
        {
            CDamageCombatLogView damageCombatLogView = await _uiFactory.CreateDamageView(component.transform);
            InitializeDamageView(damageCombatLogView, target, damage);
            component.DamageCombatLogViews.Add(damageCombatLogView);
        }

        private void InitializeDamageView(CDamageCombatLogView component, ITarget target, int damage)
        {
            Vector3 enemyPosition = target.Position.AddY(target.Height);
            Vector3 viewportPoint = _cameraService.Camera.WorldToViewportPoint(enemyPosition);
            float offset = _guiService.ScaleFactor * component.Offset;
            float dirX = viewportPoint.x > 0.5f ? 1f : -1f;
            
            Vector3 from = Vector3.zero;
            Vector3 to = new Vector3(dirX, 1f, 0f) * offset;
            Vector3 center = Vector3.Lerp(from, to, 0.25f).AddY(offset * 2f);
            
            component.Settings.From = from;
            component.Settings.Center = center;
            component.Settings.To = to;
            component.Settings.Index = 0;
            component.Settings.Target = target;
            component.Settings.IsActive = true;

            component.CanvasGroup.alpha = 0f;
            component.Text.text = damage.ToString();
        }
    }
}
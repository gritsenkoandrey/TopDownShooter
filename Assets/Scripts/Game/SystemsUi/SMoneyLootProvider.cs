using System;
using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SMoneyLootProvider : SystemComponent<CMoneyLootProvider>
    {
        private IUIFactory _uiFactory;
        private ICameraService _cameraService;
        private LootModel _lootModel;

        [Inject]
        private void Construct(IUIFactory uiFactory, ICameraService cameraService, LootModel lootModel)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _lootModel = lootModel;
        }
        
        protected override void OnEnableComponent(CMoneyLootProvider component)
        {
            base.OnEnableComponent(component);

            _lootModel.EnemyLoot
                .Subscribe(data =>
                {
                    (ITarget target, int loot) = data;
                    
                    CreateMoneyLoot(component, target, loot).Forget();
                })
                .AddTo(component.LifetimeDisposable);
        }

        private async UniTaskVoid CreateMoneyLoot(CMoneyLootProvider component, ITarget target, int loot)
        {
            if (component.MoneyLoots == Array.Empty<CMoneyLoot>())
            {
                component.MoneyLoots = new List<CMoneyLoot>();
            }
            else
            {
                for (int i = 0; i < component.MoneyLoots.Count; i++)
                {
                    if (component.MoneyLoots[i].IsActive)
                    {
                        continue;
                    }

                    ActivateMoneyLoot(component.MoneyLoots[i], target, loot);
                    return;
                }
            }
            
            CMoneyLoot moneyLoot = await _uiFactory.CreateMoneyLoot(component.transform);
            component.MoneyLoots.Add(moneyLoot);
            ActivateMoneyLoot(moneyLoot, target, loot);
        }

        private void ActivateMoneyLoot(CMoneyLoot moneyLoot, ITarget enemy, int loot)
        {
            moneyLoot.SetTarget(enemy);
            moneyLoot.Text.text = string.Format(FormatText.AddMoneyGame, loot.Trim());
            moneyLoot.CanvasGroup.alpha = 1f;
            Vector3 enemyWorldPos = moneyLoot.Target.Position.AddY(moneyLoot.Target.Height);
            Vector3 enemyScreenPos = _cameraService.Camera.WorldToScreenPoint(enemyWorldPos);
            moneyLoot.transform.position = enemyScreenPos;
            moneyLoot.SetActive(true);
        }
    }
}
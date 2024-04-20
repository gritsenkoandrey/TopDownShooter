using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Loot;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SMoneyLootProvider : SystemComponent<CMoneyLootProvider>
    {
        private IUIFactory _uiFactory;
        private ICameraService _cameraService;
        private ILootService _lootService;

        [Inject]
        private void Construct(IUIFactory uiFactory, ICameraService cameraService, ILootService lootService)
        {
            _uiFactory = uiFactory;
            _cameraService = cameraService;
            _lootService = lootService;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
                        
            _lootService.OnAddLoot += OnEnemyLoot;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _lootService.OnAddLoot -= OnEnemyLoot;
        }

        private void OnEnemyLoot((ITarget target, int loot) tuple)
        {
            foreach (CMoneyLootProvider component in Entities)
            {
                CreateMoneyLoot(component, tuple.target, tuple.loot).Forget();
            }
        }

        private async UniTaskVoid CreateMoneyLoot(CMoneyLootProvider component, ITarget target, int loot)
        {
            if (component.MoneyLoots.IsReadOnly)
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
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SMoneyLootUpdate : SystemComponent<CMoneyLoot>
    {
        private ICameraService _cameraService;
        
        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(UpdateMoneyLootPosition);
        }

        private void UpdateMoneyLootPosition(CMoneyLoot moneyLoot)
        {
            if (moneyLoot.IsActive == false)
            {
                return;
            }

            Vector3 enemyWorldPos = moneyLoot.Target.Position.AddY(moneyLoot.Target.Height);
            Vector3 enemyScreenPos = _cameraService.Camera.WorldToScreenPoint(enemyWorldPos);
            moneyLoot.CanvasGroup.alpha -= Time.deltaTime;
            moneyLoot.transform.position = enemyScreenPos;

            if (Mathf.Approximately(moneyLoot.CanvasGroup.alpha, 0f))
            {
                moneyLoot.SetActive(false);
            }
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SRegenerationHealthUpdate : SystemComponent<CRegenerationHealth>
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
            
            Entities.Foreach(Update);
        }

        private void Update(CRegenerationHealth component)
        {
            if (component.IsActive == false)
            {
                return;
            }

            Vector3 enemyWorldPos = component.Target.Position.AddY(component.Target.Height / 2f);
            Vector3 enemyScreenPos = _cameraService.Camera.WorldToScreenPoint(enemyWorldPos);
            component.CanvasGroup.alpha -= Time.deltaTime;
            component.transform.position = enemyScreenPos;

            if (Mathf.Approximately(component.CanvasGroup.alpha, 0f))
            {
                component.SetActive(false);
            }
        }
    }
}
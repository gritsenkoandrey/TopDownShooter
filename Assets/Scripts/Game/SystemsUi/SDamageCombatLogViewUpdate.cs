using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Utils;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SDamageCombatLogViewUpdate : SystemComponent<CDamageCombatLogView>
    {
        private ICameraService _cameraService;

        [Inject]
        public void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            Entities.Foreach(UpdateDamageViewPosition);
        }

        private void UpdateDamageViewPosition(CDamageCombatLogView component)
        {
            if (component.Settings.IsActive == false)
            {
                return;
            }
            
            if (component.Settings.Index > component.Points)
            {
                component.Settings.IsActive = false;
                
                return;
            }

            float elapsedTime = component.Settings.Index / (float)component.Points;
            
            Vector3 targetPosition = component.Settings.Target.Position.AddY(component.Settings.Target.Height);
            Vector3 targetScreenPosition = _cameraService.Camera.WorldToScreenPoint(targetPosition);
            
            Vector3 to = BezierCurves
                .Quadratic(component.Settings.From, component.Settings.Center, component.Settings.To, elapsedTime);

            component.transform.position = to + targetScreenPosition;
            component.CanvasGroup.alpha = Mathematics.Remap(0f, 1f, 1f, 0f, elapsedTime);
            component.Settings.Index++;
        }
    }
}
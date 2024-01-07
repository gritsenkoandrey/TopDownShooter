using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.GUI;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SPointerArrowUpdate : SystemComponent<CPointerArrow>
    {
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;

        private float _scaleFactor;

        public SPointerArrowUpdate(ICameraService cameraService, IGuiService guiService)
        {
            _cameraService = cameraService;
            _guiService = guiService;
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _scaleFactor = _guiService.StaticCanvas.Canvas.scaleFactor;
        }

        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            Entities.Foreach(UpdatePointerArrow);
        }
        
        private void UpdatePointerArrow(CPointerArrow pointerArrow)
        {
            if (!pointerArrow.Target.Health.IsAlive)
            {
                pointerArrow.CanvasGroup.alpha = 0f;
                return;
            }
            
            Vector3 indicatorPosition = _cameraService.Camera.WorldToScreenPoint(pointerArrow.Target.Position);
            Vector3 viewportPoint = _cameraService.Camera.WorldToViewportPoint(pointerArrow.Target.Position);
            
            pointerArrow.CanvasGroup.alpha = _cameraService.IsOnScreen(viewportPoint) ? 0f : 1f;

            if (indicatorPosition.z > 0f & indicatorPosition.x < pointerArrow.Rect.width * _scaleFactor
                                         & indicatorPosition.y < pointerArrow.Rect.height * _scaleFactor
                                         & indicatorPosition.x > 0f
                                         & indicatorPosition.y > 0f)
            {
                indicatorPosition.z = 0f;
            }
            else if (indicatorPosition.z > 0f)
            {
                indicatorPosition = CalculatePosition(pointerArrow, indicatorPosition);
            }
            else
            {
                indicatorPosition *= -1f;
                indicatorPosition = CalculatePosition(pointerArrow, indicatorPosition);
            }

            pointerArrow.RectTransform.position = indicatorPosition;
            pointerArrow.RectTransform.rotation = CalculateRotation(pointerArrow, indicatorPosition);
        }

        private Vector3 CalculatePosition(CPointerArrow pointerArrow, Vector3 indicatorPosition)
        {
            float offset = pointerArrow.Offset;
            Rect rect = pointerArrow.Rect;
            
            Vector3 canvasCenter = new Vector3(rect.width / 2f, rect.height / 2f, 0f) * _scaleFactor;
            
            indicatorPosition.z = 0f;
            indicatorPosition -= canvasCenter;
            float divX = (rect.width / 2f - offset) / Mathf.Abs(indicatorPosition.x);
            float divY = (rect.height / 2f - offset) / Mathf.Abs(indicatorPosition.y);
            if (divX < divY)
            {
                float angle = Vector3.SignedAngle(Vector3.right, indicatorPosition, Vector3.forward);
                indicatorPosition.x = Mathf.Sign(indicatorPosition.x) * (rect.width * 0.5f - offset) * _scaleFactor;
                indicatorPosition.y = Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.x;
            }
            else
            {
                float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition, Vector3.forward);
                indicatorPosition.y = Mathf.Sign(indicatorPosition.y) * (rect.height / 2f - offset) * _scaleFactor;
                indicatorPosition.x = -Mathf.Tan(Mathf.Deg2Rad * angle) * indicatorPosition.y;
            }
            indicatorPosition += canvasCenter;
            return indicatorPosition;
        }

        private Quaternion CalculateRotation(CPointerArrow pointerArrow, Vector3 indicatorPosition)
        {
            Rect rect = pointerArrow.Rect;
            
            Vector3 canvasCenter = new Vector3(rect.width / 2f, rect.height / 2f, 0f) * _scaleFactor;
            
            float angle = Vector3.SignedAngle(Vector3.up, indicatorPosition - canvasCenter, Vector3.forward);
            return Quaternion.Euler(new Vector3(0f, 0f, angle));
        }
    }
}
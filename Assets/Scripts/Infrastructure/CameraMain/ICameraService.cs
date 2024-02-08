using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.CameraMain
{
    public interface ICameraService
    {
        public Camera Camera { get; }
        public void Init();
        public void SetTarget(Transform target);
        public void ActivateCamera(ScreenType type);
        public void Shake();
        public bool IsOnScreen(Vector3 viewportPoint);
        public void CleanUp();
    }
}
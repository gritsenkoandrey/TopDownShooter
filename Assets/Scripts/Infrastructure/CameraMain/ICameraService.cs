using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.CameraMain
{
    public interface ICameraService
    {
        Camera Camera { get; }
        void Init();
        void SetTarget(Transform target);
        void ActivateCamera(ScreenType type);
        void Shake();
        bool IsOnScreen(Vector3 viewportPoint);
        void CleanUp();
    }
}
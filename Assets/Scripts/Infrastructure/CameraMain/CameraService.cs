using Cinemachine;
using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.CameraMain
{
    public sealed class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _cameraZoomIn;
        [SerializeField] private CinemachineVirtualCamera _cameraZoomOut;

        Camera ICameraService.Camera => _camera;
        
        void ICameraService.SetTarget(Transform target)
        {
            _cameraZoomIn.Follow = target;
            _cameraZoomIn.LookAt = target;
            _cameraZoomOut.Follow = target;
            _cameraZoomOut.LookAt = target;
        }

        void ICameraService.ActivateCamera(ScreenType type)
        {
            switch (type)
            {
                case ScreenType.None:
                case ScreenType.Lobby:
                case ScreenType.Win:
                case ScreenType.Lose:
                    _cameraZoomIn.Priority = 0;
                    _cameraZoomOut.Priority = 100;
                    break;
                case ScreenType.Game:
                    _cameraZoomIn.Priority = 100;
                    _cameraZoomOut.Priority = 0;
                    break;
            }
        }

        void ICameraService.CleanUp()
        {
            _cameraZoomIn.Follow = null;
            _cameraZoomIn.LookAt = null;
            _cameraZoomOut.Follow = null;
            _cameraZoomOut.LookAt = null;
        }
    }
}
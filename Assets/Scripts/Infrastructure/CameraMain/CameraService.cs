using UnityEngine;

namespace CodeBase.Infrastructure.CameraMain
{
    public sealed class CameraService : MonoBehaviour, ICameraService
    {
        [SerializeField] private Camera _camera;

        Camera ICameraService.Camera => _camera;
    }
}
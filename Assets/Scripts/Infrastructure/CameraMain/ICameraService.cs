using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.CameraMain
{
    public interface ICameraService : IService
    {
        public Camera Camera { get; }
    }
}
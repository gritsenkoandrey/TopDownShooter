using Cinemachine;
using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CVirtualCamera : EntityComponent<CVirtualCamera>
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public void SetTarget(Transform target)
        {
            _virtualCamera.Follow = target;
            _virtualCamera.LookAt = target;
        }
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
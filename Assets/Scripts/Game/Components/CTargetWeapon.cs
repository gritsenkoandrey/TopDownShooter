using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CTargetWeapon : EntityComponent<CTargetWeapon>, IPause
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _targetScale;

        public Transform Transform => _transform;
        public Transform TargetScale => _targetScale;
        public float Scale => _transform.localScale.x;
        public ITarget Target { get; private set; }
        public bool HasTarget { get; private set; }
        public bool IsValid { get; private set; }
        public bool IsPause { get; private set; }

        public void SetTarget(ITarget target)
        {
            if (target != null)
            {
                Target = target;
                HasTarget = true;
                IsValid = true;
            }
            else
            {
                IsValid = false;
            }
        }

        void IPause.Pause(bool isPause) => IsPause = isPause;
    }
}
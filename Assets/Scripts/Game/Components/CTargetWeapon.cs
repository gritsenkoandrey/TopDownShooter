using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CTargetWeapon : EntityComponent<CTargetWeapon>
    {
        [SerializeField] private Transform _transform;

        public Transform Transform => _transform;
        public float Scale => _transform.localScale.x;
        public ITarget Target { get; set; }
        public bool HasTarget { get; set; }
        public bool IsValid { get; set; }
    }
}
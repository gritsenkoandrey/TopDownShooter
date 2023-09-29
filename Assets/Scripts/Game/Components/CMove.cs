using CodeBase.ECSCore;
using CodeBase.Infrastructure.Progress;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CMove : EntityComponent<CMove>, IProgressReader
    {
        [SerializeField] private CharacterController _characterController;

        public CharacterController CharacterController => _characterController;
        public float BaseSpeed { get; set; }
        public float Speed { get; private set; }
        public float Velocity => _characterController.velocity.sqrMagnitude;
        public bool IsGrounded => _characterController.isGrounded;
        
        void IProgressReader.Read(PlayerProgress progress)
        {
            Speed = progress.Stats.Speed + BaseSpeed;
        }
    }
}
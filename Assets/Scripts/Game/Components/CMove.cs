using CodeBase.ECSCore;
using CodeBase.Infrastructure.Progress;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CMove : EntityComponent<CMove>, IProgressReader
    {
        [SerializeField] private CharacterController _characterController;

        public CharacterController CharacterController => _characterController;
        public Vector2 Input { get; set; }
        public float BaseSpeed { get; set; }
        public float Speed { get; private set; }
        
        void IProgressReader.Read(PlayerProgress progress)
        {
            Speed = progress.Stats.Speed + BaseSpeed;
        }

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
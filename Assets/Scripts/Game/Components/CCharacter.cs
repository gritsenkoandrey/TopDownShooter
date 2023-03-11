using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>
    {
        [SerializeField] private CharacterController _characterController;

        public CharacterController CharacterController => _characterController;
        public Vector3 Position => transform.position;
        public ReactiveCommand Move { get; } = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
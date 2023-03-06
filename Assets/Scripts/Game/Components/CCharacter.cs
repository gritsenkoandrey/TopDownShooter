using AndreyGritsenko.ECSCore;
using UnityEngine;

namespace AndreyGritsenko.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>
    {
        [SerializeField] private CharacterController _characterController;

        public CharacterController CharacterController => _characterController;

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
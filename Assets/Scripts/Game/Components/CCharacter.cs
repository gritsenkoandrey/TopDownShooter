using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>
    {
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CHealth _health;
        [SerializeField] private CWeapon _weapon;
        [SerializeField] private CMove _move;
        
        public CAnimator Animator => _animator;
        public CHealth Health => _health;
        public CWeapon Weapon => _weapon;
        public CMove Move => _move;
        public Vector3 Position => transform.position;

        public ReactiveCollection<CZombie> Enemies { get; } = new();
        public ReactiveCommand UpdateStateMachine { get; } = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
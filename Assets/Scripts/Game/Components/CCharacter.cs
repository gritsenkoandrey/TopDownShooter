using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>, ICharacter
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CHealth _health;
        [SerializeField] private CWeapon _weapon;
        [SerializeField] private CHealthView _healthView;
        
        public CharacterController CharacterController => _characterController;
        public CAnimator Animator => _animator;
        public CHealth Health => _health;
        public CWeapon Weapon => _weapon;
        public CHealthView HealthView => _healthView;
        public Vector3 Position => transform.position;
        public GameObject Object => gameObject;
        public Vector2 Input { get; set; }

        public ReactiveCollection<CEnemy> Enemies { get; } = new();
        public ReactiveCommand UpdateStateMachine { get; } = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
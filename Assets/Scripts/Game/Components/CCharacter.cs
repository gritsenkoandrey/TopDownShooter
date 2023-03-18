using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>, ICharacter
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _animator;
        [SerializeField] private CHealth _health;
        [FormerlySerializedAs("_attack")] [SerializeField] private CWeapon weapon;
        
        public CharacterController CharacterController => _characterController;
        public Animator Animator => _animator;
        public CHealth Health => _health;
        public CWeapon Weapon => weapon;
        public Vector3 Position => transform.position;
        public GameObject Object => gameObject;
        public Vector2 Value { get; set; }

        public ReactiveCollection<CEnemy> Enemies { get; } = new();
        public ReactiveCommand UpdateStateMachine { get; } = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
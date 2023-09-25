using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>, ICharacter
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
        public ReactiveCommand UpdateStateMachine { get; } = new();
    }
}
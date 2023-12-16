using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Models;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacter : EntityComponent<CCharacter>, ICharacter
    {
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CWeaponMediator _weaponMediator;
        [SerializeField] private CMove _move;
        [SerializeField] private CStateMachine _stateMachine;

        public CAnimator Animator => _animator;
        public CWeaponMediator WeaponMediator => _weaponMediator;
        public CMove Move => _move;
        public CStateMachine StateMachine => _stateMachine;
        public Health Health { get; } = new();
        public Entity Entity => this;
    } 
}
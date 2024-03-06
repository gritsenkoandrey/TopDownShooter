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
        [SerializeField] private CCharacterController _characterController;
        [SerializeField] private CStateMachine _stateMachine;
        [SerializeField] private CBodyMediator _bodyMediator;

        public CAnimator Animator => _animator;
        public CWeaponMediator WeaponMediator => _weaponMediator;
        public CCharacterController CharacterController => _characterController;
        public CStateMachine StateMachine => _stateMachine;
        public CBodyMediator BodyMediator => _bodyMediator;
        public Health Health { get; } = new();
        public Vector3 Position => transform.position;
        public float Height => 3f;
    } 
}
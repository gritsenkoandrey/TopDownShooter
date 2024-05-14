using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CUnit : EntityComponent<CUnit>, IEnemy
    {
        [SerializeField] private CAgent _agent;
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CWeaponMediator _weaponMediator;
        [SerializeField] private CStateMachine _stateMachine;
        [SerializeField] private CBodyMediator _bodyMediator;
        [SerializeField] private CShadow _shadow;
        [SerializeField] private CHealth _health;

        public CAgent Agent => _agent;
        public CAnimator Animator => _animator;
        public CWeaponMediator WeaponMediator => _weaponMediator;
        public CStateMachine StateMachine => _stateMachine;
        public CBodyMediator BodyMediator => _bodyMediator;
        public CShadow Shadow => _shadow;
        public CHealth Health => _health;
        public UnitStats UnitStats { get; set; }
        public Vector3 Position => transform.position;
        public Vector3 Forward => transform.forward;
        public float Height => UnitStats.Height;
        public float Scale => UnitStats.Scale;
        public int Loot => UnitStats.Money;
    }
}
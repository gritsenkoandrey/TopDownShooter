using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Models;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CUnit : EntityComponent<CUnit>, IEnemy
    {
        [SerializeField] private CAgent _agent;
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CWeaponMediator _weaponMediator;
        [SerializeField] private CRadar _radar;
        [SerializeField] private CStateMachine _stateMachine;
        [SerializeField] private CBodyMediator _bodyMediator;

        public CAgent Agent => _agent;
        public CAnimator Animator => _animator;
        public CWeaponMediator WeaponMediator => _weaponMediator;
        public CRadar Radar => _radar;
        public CStateMachine StateMachine => _stateMachine;
        public CBodyMediator BodyMediator => _bodyMediator;
        public Health Health { get; } = new ();
        public UnitStats UnitStats { get; set; }
        public Vector3 Position => transform.position;
        public float Height => UnitStats.Height;
        public int Loot => UnitStats.Money;
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Models;
using CodeBase.Infrastructure.StaticData.Data;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Components
{
    [RequireComponent(typeof(Animator))]
    public sealed class CZombie : EntityComponent<CZombie>, IEnemy
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CRadar _radar;
        [SerializeField] private CStateMachine _stateMachine;

        public NavMeshAgent Agent => _agent;
        public CAnimator Animator => _animator;
        public CRadar Radar => _radar;
        public CStateMachine StateMachine => _stateMachine;
        public Health Health { get; } = new();
        public Vector3 Position => transform.position;
        public float Height => Stats.Height;
        public int Money => Stats.Money;
        public EnemyStats Stats { get; private set; }
        public int Damage { get; private set; }
        public ReactiveCommand Attack { get; } = new();
        public ReactiveCommand OnCheckDamage { get; } = new();

        /// <summary>
        /// Animation event
        /// </summary>
        public void OnAttack() => OnCheckDamage.Execute();
        public void SetStats(EnemyStats stats) => Stats = stats;
        public void SetDamage(int damage) => Damage = damage;
    }
}
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
        public Health Health { get; } = new();
        public CStateMachine StateMachine => _stateMachine;
        public EnemyStats Stats { get; set; }
        public Vector3 Position => transform.position;
        public int Damage { get; set; }
        public ReactiveCommand Attack { get; } = new();
        public ReactiveCommand OnCheckDamage { get; } = new();
        public void OnAttack() => OnCheckDamage.Execute();
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Components
{
    public sealed class CEnemy : EntityComponent<CEnemy>, IHealth
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private CRadar _radar;
        [SerializeField] private CHealth _health;
        [SerializeField] private CAttack _attack;

        public NavMeshAgent Agent => _agent;
        public Animator Animator => _animator;
        public CRadar Radar => _radar;
        public CHealth Health => _health;
        public CAttack Attack => _attack;
        public Vector3 Position => transform.position;

        public ReactiveCommand UpdateStateMachine { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
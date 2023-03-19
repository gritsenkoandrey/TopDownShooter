using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Components
{
    public sealed class CEnemy : EntityComponent<CEnemy>, IZombie
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CAnimator _animator;
        [SerializeField] private CRadar _radar;
        [SerializeField] private CHealth _health;
        [SerializeField] private CMelee _melee;
        [SerializeField] private CHealthView _healthView;

        public NavMeshAgent Agent => _agent;
        public CAnimator Animator => _animator;
        public CRadar Radar => _radar;
        public CHealth Health => _health;
        public CMelee Melee => _melee;
        public CHealthView HealthView => _healthView;
        public Vector3 Position => transform.position;
        public bool IsAggro { get; set; }
        public void OnAttack() => _melee.OnAttack.Execute();
        public ReactiveCommand UpdateStateMachine { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
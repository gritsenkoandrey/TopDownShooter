using CodeBase.ECSCore;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Components
{
    public sealed class CEnemy : EntityComponent<CEnemy>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Collider _collider;
        [SerializeField] private CRadar _radar;

        public NavMeshAgent Agent => _agent;
        public Collider Collider => _collider;
        public CRadar Radar => _radar;
        public Vector3 Position => transform.position;

        public ReactiveCommand UpdateStateMachine { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
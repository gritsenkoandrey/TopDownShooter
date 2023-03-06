using AndreyGritsenko.ECSCore;
using UnityEngine;
using UnityEngine.AI;

namespace AndreyGritsenko.Game.Components
{
    public sealed class CEnemy : EntityComponent<CEnemy>
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Collider _collider;

        public NavMeshAgent Agent => _agent;
        public Collider Collider => _collider;
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
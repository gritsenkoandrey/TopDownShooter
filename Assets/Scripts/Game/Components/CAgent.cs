using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Components
{
    public sealed class CAgent : EntityComponent<CAgent>, IPause
    {
        [SerializeField] private NavMeshAgent _agent;

        public NavMeshAgent Agent => _agent;

        private float _speed;
        
        void IPause.Pause(bool isPause)
        {
            if (isPause)
            {
                _speed = _agent.speed;
                _agent.speed = 0f;
            }
            else
            {
                _agent.speed = _speed;
            }
        }
    }
}
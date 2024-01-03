using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CZombieSpawner : EntityComponent<CZombieSpawner>
    {
        [SerializeField] private ZombieType _zombieType;

        public ZombieType ZombieType => _zombieType;
        public Vector3 Position => transform.position;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
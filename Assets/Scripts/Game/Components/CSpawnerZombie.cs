using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CSpawnerZombie : EntityComponent<CSpawnerZombie>, IPosition
    {
        [SerializeField] private ZombieType _zombieType;

        public ZombieType ZombieType => _zombieType;
        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawSphere(transform.position, 0.25f);
        }

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
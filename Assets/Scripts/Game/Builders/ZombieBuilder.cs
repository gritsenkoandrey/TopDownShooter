using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class ZombieBuilder
    {
        private CZombie _prefab;
        private Transform _parent;
        
        private EnemyStats _stats;
        private int _health;
        private int _damage;
        private Vector3 _position;

        public ZombieBuilder SetPrefab(CZombie prefab)
        {
            _prefab = prefab;

            return this;
        }

        public ZombieBuilder SetPosition(Vector3 position)
        {
            _position = position;

            return this;
        }

        public ZombieBuilder SetParent(Transform parent)
        {
            _parent = parent;

            return this;
        }

        public ZombieBuilder SetHealth(int health)
        {
            _health = health;

            return this;
        }

        public ZombieBuilder SetDamage(int damage)
        {
            _damage = damage;

            return this;
        }

        public ZombieBuilder SetStats(EnemyStats stats)
        {
            _stats = stats;

            return this;
        }

        public ZombieBuilder Reset()
        {
            _prefab = null;
            _parent = null;
            _stats = default;
            _position = default;
            _health = default;
            _damage = default;

            return this;
        }

        public CZombie Build()
        {
            CZombie zombie = Object.Instantiate(_prefab, _position, Quaternion.identity, _parent);

            zombie.Health.MaxHealth = _health;
            zombie.Health.Health.Value = _health;
            zombie.Melee.Damage = _damage;
            zombie.Stats = _stats;
            zombie.Radar.Radius = _stats.AggroRadius;

            return zombie;
        }
    }
}
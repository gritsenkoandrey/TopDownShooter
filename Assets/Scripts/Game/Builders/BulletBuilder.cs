using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Pool;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class BulletBuilder
    {
        private GameObject _prefab;
        private float _collisionDistance;
        private Vector3 _position;
        private Vector3 _direction;
        private int _damage;

        private readonly IObjectPoolService _objectPoolService;

        public BulletBuilder(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }

        public BulletBuilder SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
            
            return this;
        }

        public BulletBuilder SetCollisionDistance(float collisionDistance)
        {
            _collisionDistance = collisionDistance;
            
            return this;
        }

        public BulletBuilder SetPosition(Vector3 position)
        {
            _position = position;

            return this;
        }

        public BulletBuilder SetDirection(Vector3 direction)
        {
            _direction = direction;
            
            return this;
        }

        public BulletBuilder SetDamage(int damage)
        {
            _damage = damage;
            
            return this;
        }

        public IBullet Build()
        {
            IBullet bullet = _objectPoolService
                .SpawnObject(_prefab, _position, Quaternion.identity)
                .GetComponent<IBullet>();

            bullet.Damage = _damage;
            bullet.SetDirection(_direction);
            bullet.SetCollisionDistance(_collisionDistance);

            return bullet;
        }
    }
}
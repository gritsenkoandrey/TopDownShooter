using CodeBase.Game.Components;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders.Projectile
{
    public sealed class ProjectileBuilder
    {
        private GameObject _prefab;
        private Transform _spawnPoint;
        private ProjectileData _data;
        private Vector3 _position;
        private Vector3 _direction;
        private Quaternion _rotation;
        private int _damage;

        private readonly IObjectPoolService _objectPoolService;

        public ProjectileBuilder(IObjectPoolService objectPoolService)
        {
            _objectPoolService = objectPoolService;
        }

        public ProjectileBuilder SetPrefab(GameObject prefab)
        {
            _prefab = prefab;
            return this;
        }

        public ProjectileBuilder SetSpawnPoint(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
            return this;
        }

        public ProjectileBuilder SetDirection(Vector3 direction)
        {
            _direction = direction;
            return this;
        }

        public ProjectileBuilder SetDamage(int damage)
        {
            _damage = damage;
            return this;
        }

        public ProjectileBuilder SetData(ProjectileData data)
        {
            _data = data;
            return this;
        }

        public CBullet Build()
        {
            Vector3 position = _spawnPoint.position;
            Quaternion rotation = _spawnPoint.rotation;
            
            CBullet bullet = _objectPoolService
                .SpawnObject(_prefab, position, rotation)
                .GetComponent<CBullet>();

            bullet.SetDamage(_damage);
            bullet.SetDirection(_direction);
            bullet.SetCollisionDistance(Mathf.Pow(_data.CollisionRadius, 2));

            return bullet;
        }
    }
}
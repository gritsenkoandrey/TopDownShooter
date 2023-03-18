using CodeBase.ECSCore;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeapon : EntityComponent<CWeapon>
    {
        [SerializeField] private Transform _spawnBulletPoint;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;

        public Transform SpawnBulletPoint => _spawnBulletPoint;
        public float Speed => _speed;
        public int Damage => _damage;
        public ReactiveCommand Shoot { get; } = new();
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
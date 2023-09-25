using CodeBase.ECSCore;
using CodeBase.Infrastructure.Progress;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeapon : EntityComponent<CWeapon>, IProgressReader
    {
        [SerializeField] private Transform _spawnBulletPoint;
        [SerializeField] private float _force = 30f;

        public Transform SpawnBulletPoint => _spawnBulletPoint;
        public float Force => _force;
        public float AttackDistance { get; set; }
        public float AttackRecharge { get; set; }
        public int BaseDamage { get; set; }
        public int Damage { get; private set; }
        public ReactiveCommand Shoot { get; } = new();
        
        void IProgressReader.Read(PlayerProgress progress)
        {
            Damage = progress.Stats.Damage * BaseDamage;
        }
    }
}
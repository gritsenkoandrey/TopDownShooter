using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeapon : EntityComponent<CWeapon>
    {
        [SerializeField] private ProjectileType _projectileType;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private RuntimeAnimatorController _runtimeAnimatorController;

        public ProjectileType ProjectileType => _projectileType;
        public Transform[] SpawnPoints => _spawnPoints;
        public RuntimeAnimatorController RuntimeAnimatorController => _runtimeAnimatorController;
        public IWeapon Weapon { get; private set; }
        
        public void SetWeapon(IWeapon weapon)
        {
            Weapon?.Dispose();
            Weapon = weapon;
        }
    }
}
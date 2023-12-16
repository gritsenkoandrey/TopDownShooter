using CodeBase.ECSCore;
using CodeBase.Game.Weapon;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeapon : EntityComponent<CWeapon>
    {
        [SerializeField] private Transform _spawnBulletPoint;
        [SerializeField] private WeaponType _weaponType;
        public WeaponType WeaponType => _weaponType;
        public Vector3 SpawnBulletPointPosition => _spawnBulletPoint.position;
        public Vector3 NormalizeForwardDirection => _spawnBulletPoint.forward.normalized;
        public IWeapon Weapon { get; set; }
    }
}
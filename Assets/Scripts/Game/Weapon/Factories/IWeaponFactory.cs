using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Game.Weapon.Factories
{
    public interface IWeaponFactory : IService
    {
        public UniTask<CWeapon> CreateCharacterWeapon(WeaponType type, Transform parent);
        public UniTask<CWeapon> CreateUnitWeapon(WeaponType type, Transform parent);
        public UniTask<IBullet> CreateProjectile(ProjectileType type, Transform spawnPoint, int damage, Vector3 direction);
    }
}
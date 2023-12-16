using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon.Data;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Game.Weapon.Factories
{
    public interface IWeaponFactory : IService
    {
        public BaseWeapon GetWeapon(CWeapon weaponComponent, WeaponType type);
        public WeaponCharacteristic GetWeaponCharacteristic(WeaponType type);
        public UniTask<IBullet> CreateBullet(int damage, Vector3 position, Vector3 direction);
    }
}
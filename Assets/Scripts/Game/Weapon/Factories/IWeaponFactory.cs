using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Game.Weapon.Factories
{
    public interface IWeaponFactory : IService
    {
        public UniTask<CWeapon> CreateWeapon(WeaponType type, Transform parent);
        public UniTask<IBullet> CreateBullet(int damage, Vector3 position, Vector3 direction);
    }
}
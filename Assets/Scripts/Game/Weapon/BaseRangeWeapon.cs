using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseRangeWeapon : BaseWeapon
    {
        private protected IWeaponFactory WeaponFactory;
        
        protected BaseRangeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
        }

        public override void Attack(ITarget target = null)
        {
            base.Attack(target);
            
            Shoot();
        }

        private void Shoot()
        {
            CreateBullet().Forget();
        }

        private async UniTaskVoid CreateBullet()
        {
            for (int i = 0; i < Weapon.SpawnPoints.Length; i++)
            {
                Vector3 normalized = Weapon.SpawnPoints[i].forward.normalized;
                Vector3 direction = new Vector3(normalized.x, 0f, normalized.z) * WeaponCharacteristic.ForceBullet;
                int damage = CalculateCriticalDamage(GetDamage());
                await WeaponFactory.CreateProjectile(Weapon.ProjectileType, Weapon.SpawnPoints[i], damage, direction);
            }
        }
    }
}
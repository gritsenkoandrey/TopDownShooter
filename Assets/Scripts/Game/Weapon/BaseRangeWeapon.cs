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
            NotReadyAttack();
            ReduceClip();
            UpdateFireInterval();

            if (ClipIsEmpty())
            {
                UpdateRechargeTime();
            }
        }

        private async UniTaskVoid CreateBullet()
        {
            int damage = GetDamage(null);
            
            for (int i = 0; i < Weapon.SpawnPoints.Length; i++)
            {
                Vector3 normalized = Weapon.SpawnPoints[i].forward.normalized;
                Vector3 direction = new Vector3(normalized.x, 0f, normalized.z) * WeaponCharacteristic.ForceBullet;
            
                await WeaponFactory.CreateProjectile(Weapon.ProjectileType, Weapon.SpawnPoints[i], 
                    CalculateCriticalDamage(damage), direction);
            }
        }

        private int CalculateCriticalDamage(int damage)
        {
            bool isCriticalDamage = WeaponCharacteristic.CriticalChance > Random.Range(0, 100);

            if (isCriticalDamage)
            {
                return Mathf.RoundToInt(damage * WeaponCharacteristic.CriticalMultiplier);
            }

            return damage;
        }
    }
}
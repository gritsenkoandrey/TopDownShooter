using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.Builders.Weapon
{
    public abstract class BaseWeaponBuilder
    {
        private protected readonly IObjectResolver ObjectResolver;
        private protected readonly WeaponCharacteristic WeaponCharacteristic;
        
        private protected GameObject Prefab;
        private protected Transform Parent;
        private protected WeaponType WeaponType;

        protected BaseWeaponBuilder(IObjectResolver objectResolver, WeaponCharacteristic weaponCharacteristic)
        {
            ObjectResolver = objectResolver;
            WeaponCharacteristic = weaponCharacteristic;
        }

        public BaseWeaponBuilder SetPrefab(GameObject prefab)
        {
            Prefab = prefab;
            return this;
        }

        public BaseWeaponBuilder SetParent(Transform parent)
        {
            Parent = parent;
            return this;
        }

        public BaseWeaponBuilder SetWeaponType(WeaponType weaponType)
        {
            WeaponType = weaponType;
            return this;
        }

        public abstract CWeapon Build();
    }
}
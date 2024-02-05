using CodeBase.Game.Components;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using UnityEngine;

namespace CodeBase.Game.Builders.Weapon
{
    public abstract class BaseWeaponBuilder
    {
        private protected GameObject Prefab;
        private protected Transform Parent;
        private protected WeaponType WeaponType;

        protected BaseWeaponBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic) { }

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
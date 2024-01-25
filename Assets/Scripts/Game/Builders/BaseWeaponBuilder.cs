using CodeBase.Game.Components;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public abstract class BaseWeaponBuilder
    {
        private protected GameObject Prefab;
        private protected Transform Parent;

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

        public abstract CWeapon Build();
    }
}
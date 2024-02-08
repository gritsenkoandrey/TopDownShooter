using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Weapon;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders.Weapon
{
    public abstract class BaseWeaponBuilder
    {
        private protected GameObject Prefab;
        private protected Transform Parent;
        private protected WeaponType WeaponType;

        protected BaseWeaponBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic, IEffectFactory effectFactory) { }

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
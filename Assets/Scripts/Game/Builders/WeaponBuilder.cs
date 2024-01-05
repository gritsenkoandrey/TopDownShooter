using CodeBase.Game.Components;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.Data;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using UnityEngine;

namespace CodeBase.Game.Builders
{
    public sealed class WeaponBuilder
    {
        private GameObject _prefab;
        private Transform _parent;
        private WeaponType _weaponType;
        
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly InventoryModel _inventoryModel;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IProgressService _progressService;

        public WeaponBuilder(IWeaponFactory weaponFactory, IProgressService progressService, 
            WeaponCharacteristic weaponCharacteristic, InventoryModel inventoryModel)
        {
            _weaponFactory = weaponFactory;
            _progressService = progressService;
            _weaponCharacteristic = weaponCharacteristic;
            _inventoryModel = inventoryModel;
        }

        public WeaponBuilder SetPrefab(GameObject prefab)
        {
            _prefab = prefab;

            return this;
        }

        public WeaponBuilder SetParent(Transform parent)
        {
            _parent = parent;

            return this;
        }

        public WeaponBuilder SetWeaponType(WeaponType weaponType)
        {
            _weaponType = weaponType;

            return this;
        }

        public CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(_prefab, _parent).GetComponent<CWeapon>();

            IWeapon rangeWeapon = new RangeWeapon(weapon, _weaponFactory, _progressService, _weaponCharacteristic, _inventoryModel);
            
            weapon.SetWeapon(rangeWeapon);

            return weapon;
        }
    }
}
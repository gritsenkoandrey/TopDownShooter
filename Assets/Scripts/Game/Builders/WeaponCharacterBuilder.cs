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
    public sealed class WeaponCharacterBuilder : BaseWeaponBuilder
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly IProgressService _progressService;
        private readonly InventoryModel _inventoryModel;
        private readonly DamageCombatLog _damageCombatLog;

        public WeaponCharacterBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic, DamageCombatLog damageCombatLog,
            IProgressService progressService, InventoryModel inventoryModel) 
            : base(weaponFactory, weaponCharacteristic, damageCombatLog)
        {
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponCharacteristic;
            _damageCombatLog = damageCombatLog;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();

            IWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                currentWeapon = new CharacterMeleeWeapon(weapon, _weaponCharacteristic, _damageCombatLog, _progressService, _inventoryModel);
            }
            else
            {
                currentWeapon = new CharacterRangeWeapon(weapon, _weaponFactory, _weaponCharacteristic, _inventoryModel, _progressService);
            }
            
            weapon.SetWeapon(currentWeapon);
            return weapon;
        }
    }
}
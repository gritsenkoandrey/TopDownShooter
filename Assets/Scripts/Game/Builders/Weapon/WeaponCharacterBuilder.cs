using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon;
using CodeBase.Game.Weapon.SpecificWeapons;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Builders.Weapon
{
    public sealed class WeaponCharacterBuilder : BaseWeaponBuilder
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly IEffectFactory _effectFactory;
        private readonly IProgressService _progressService;
        private readonly InventoryModel _inventoryModel;
        private readonly DamageCombatLog _damageCombatLog;

        public WeaponCharacterBuilder(IWeaponFactory weaponFactory, WeaponCharacteristic weaponCharacteristic, DamageCombatLog damageCombatLog,
            IProgressService progressService, InventoryModel inventoryModel, IEffectFactory effectFactory) 
            : base(weaponFactory, weaponCharacteristic, effectFactory)
        {
            _weaponFactory = weaponFactory;
            _weaponCharacteristic = weaponCharacteristic;
            _damageCombatLog = damageCombatLog;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
            _effectFactory = effectFactory;
        }

        public override CWeapon Build()
        {
            CWeapon weapon = Object.Instantiate(Prefab, Parent).GetComponent<CWeapon>();

            IWeapon currentWeapon;

            if (WeaponType == WeaponType.Knife)
            {
                currentWeapon = new CharacterMeleeWeapon(weapon, _weaponCharacteristic, _damageCombatLog, _progressService, _inventoryModel, _effectFactory);
            }
            else
            {
                currentWeapon = new CharacterRangeWeapon(weapon, _weaponCharacteristic, _weaponFactory, _inventoryModel, _progressService);
            }
            
            weapon.SetWeapon(currentWeapon);
            return weapon;
        }
    }
}
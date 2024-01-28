using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Weapon.Data;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class CharacterMeleeWeapon : MeleeWeapon
    {
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly IProgressService _progressService;
        private readonly InventoryModel _inventoryModel;

        public CharacterMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic, DamageCombatLog damageCombatLog, 
            IProgressService progressService, InventoryModel inventoryModel)
            : base(weapon, weaponCharacteristic, damageCombatLog)
        {
            _weaponCharacteristic = weaponCharacteristic;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
            
            ReloadClip();
        }

        private protected override int SetDamage()
        {
            return _weaponCharacteristic.Damage * _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Damage];
        }

        private protected override void ReloadClip()
        {
            base.ReloadClip();
            
            _inventoryModel.ClipCount.Value = _weaponCharacteristic.ClipCount;
        }

        private protected override void ReduceClip()
        {
            base.ReduceClip();

            _inventoryModel.ClipCount.Value--;
        }
    }
}
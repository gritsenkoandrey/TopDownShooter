using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData.Data;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class CharacterRangeWeapon : RangeWeapon, IWeapon
    {
        private readonly IProgressService _progressService;
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly InventoryModel _inventoryModel;
        
        public CharacterRangeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic, IWeaponFactory weaponFactory, 
            InventoryModel inventoryModel, IProgressService progressService)
            : base(weapon, weaponFactory, weaponCharacteristic)
        {
            _weaponCharacteristic = weaponCharacteristic;
            _inventoryModel = inventoryModel;
            _progressService = progressService;
            
            ReloadClip();
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

            if (_inventoryModel.ClipCount.Value <= 0)
            {
                _inventoryModel.Reloading.Execute(_weaponCharacteristic.RechargeTime);
            }
        }

        private protected override int SetDamage()
        {
            return _weaponCharacteristic.Damage * _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Damage];
        }
    }
}
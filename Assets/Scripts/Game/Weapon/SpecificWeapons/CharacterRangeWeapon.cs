using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData.Data;
using VContainer;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class CharacterRangeWeapon : RangeWeapon
    {
        private IProgressService _progressService;
        private InventoryModel _inventoryModel;

        public CharacterRangeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
            Weapon = weapon;
            WeaponCharacteristic = weaponCharacteristic;
        }

        [Inject]
        private void Construct(IWeaponFactory weaponFactory, InventoryModel inventoryModel, IProgressService progressService)
        {
            WeaponFactory = weaponFactory;
            
            _progressService = progressService;
            _inventoryModel = inventoryModel;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            ReloadClip();
        }

        private protected override void ReloadClip()
        {
            base.ReloadClip();
            
            _inventoryModel.ClipCount.Value = WeaponCharacteristic.ClipCount;
        }

        private protected override void ReduceClip()
        {
            base.ReduceClip();
            
            _inventoryModel.ClipCount.Value--;

            if (_inventoryModel.ClipCount.Value <= 0)
            {
                _inventoryModel.Reloading.Execute(WeaponCharacteristic.RechargeTime);
            }
        }

        private protected override int SetDamage()
        {
            return WeaponCharacteristic.Damage * _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Damage];
        }
    }
}
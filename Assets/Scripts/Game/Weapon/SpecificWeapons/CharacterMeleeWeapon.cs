using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Game.Weapon.Data;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class CharacterMeleeWeapon : MeleeWeapon
    {
        private readonly WeaponCharacteristic _weaponCharacteristic;
        private readonly IProgressService _progressService;
        private readonly InventoryModel _inventoryModel;
        private readonly DamageCombatLog _damageCombatLog;

        public CharacterMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic, DamageCombatLog damageCombatLog, 
            IProgressService progressService, InventoryModel inventoryModel, IEffectFactory effectFactory)
            : base(weapon, weaponCharacteristic, effectFactory)
        {
            _weaponCharacteristic = weaponCharacteristic;
            _damageCombatLog = damageCombatLog;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
            
            ReloadClip();
        }

        private protected override int SetDamage(ITarget target)
        {
            int damage = _weaponCharacteristic.Damage * _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Damage];
            
            _damageCombatLog.AddLog(target, damage);
            
            return damage;
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
    }
}
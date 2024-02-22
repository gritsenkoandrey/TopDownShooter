using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData.Data;
using VContainer;

namespace CodeBase.Game.Weapon.SpecificWeapons
{
    public sealed class CharacterMeleeWeapon : BaseMeleeWeapon
    {
        private IProgressService _progressService;
        private DamageCombatLog _damageCombatLog;
        private InventoryModel _inventoryModel;

        public CharacterMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
            Weapon = weapon;
            WeaponCharacteristic = weaponCharacteristic;
        }

        [Inject]
        private void Construct(IEffectFactory effectFactory, IProgressService progressService, 
            DamageCombatLog damageCombatLog, InventoryModel inventoryModel)
        {
            EffectFactory = effectFactory;
            
            _progressService = progressService;
            _damageCombatLog = damageCombatLog;
            _inventoryModel = inventoryModel;
        }
        
        public override void Initialize()
        {
            base.Initialize();
            
            ReloadClip();
        }

        private protected override int GetDamage()
        {
            return WeaponCharacteristic.Damage * _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Damage];
        }

        private protected override void SendCombatLog(ITarget target, int damage)
        {
            base.SendCombatLog(target, damage);
            
            _damageCombatLog.AddLog(target, damage);
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
                _inventoryModel.ReloadingWeapon.Execute(WeaponCharacteristic.RechargeTime);
            }
        }
    }
}
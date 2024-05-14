using System;
using System.Globalization;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopCharacterStats : SystemComponent<CCharacterStats>
    {
        private IStaticDataService _staticDataService;
        private IProgressService _progressService;
        private InventoryModel _inventoryModel;

        private const string FixedPoint = "F1";

        [Inject]
        private void Construct(IStaticDataService staticDataService, IProgressService progressService, InventoryModel inventoryModel)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
        }

        protected override void OnEnableComponent(CCharacterStats component)
        {
            base.OnEnableComponent(component);

            void UpdateHealthStat(SkinType type, int multiplier)
            {
                SkinCharacteristicData data = _staticDataService.SkinCharacteristicData(type);
                string health = (data.SkinCharacteristic.BaseHealth * multiplier).ToString();
                string hps = (data.SkinCharacteristic.RegenerationHealth / data.SkinCharacteristic.RegenearationInterval)
                    .ToString(FixedPoint, CultureInfo.InvariantCulture);
                component.TextHealth.text = string.Format(FormatText.HealthStat, health, hps);
            }

            void UpdateDamageStat(WeaponType type, int multiplier)
            {
                WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);
                string damage = (data.WeaponCharacteristic.Damage * multiplier).ToString();
                string dps = (data.WeaponCharacteristic.Damage / data.WeaponCharacteristic.FireInterval)
                    .ToString(FixedPoint, CultureInfo.InvariantCulture);
                component.TextDamage.text = string.Format(FormatText.DamageStat, damage, dps);
            }

            IObservable<int> healthSubscribe = _progressService.StatsData.Data.Value.Data
                .ObserveEveryValueChanged(data => data[UpgradeButtonType.Health]);

            IObservable<int> damageSubscribe = _progressService.StatsData.Data.Value.Data
                .ObserveEveryValueChanged(data => data[UpgradeButtonType.Damage]);
            
            _inventoryModel.SelectedSkin
                .CombineLatest(healthSubscribe, (skinType, multiplier) => (skinType, multiplier))
                .Subscribe(tuple => UpdateHealthStat(tuple.skinType, tuple.multiplier))
                .AddTo(component.LifetimeDisposable);

            _inventoryModel.SelectedWeapon
                .CombineLatest(damageSubscribe, (weaponType, multiplier) => (weaponType, multiplier))
                .Subscribe(tuple => UpdateDamageStat(tuple.weaponType, tuple.multiplier))
                .AddTo(component.LifetimeDisposable);
        }
    }
}
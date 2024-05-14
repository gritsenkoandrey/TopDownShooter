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

        [Inject]
        private void Construct(IStaticDataService staticDataService, IProgressService progressService, 
            InventoryModel inventoryModel)
        {
            _staticDataService = staticDataService;
            _progressService = progressService;
            _inventoryModel = inventoryModel;
        }

        protected override void OnEnableComponent(CCharacterStats component)
        {
            base.OnEnableComponent(component);

            void UpdateHealthStat(SkinType type)
            {
                SkinCharacteristicData data = _staticDataService.SkinCharacteristicData(type);
                int healthMultiplier = _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Health];

                string health = (data.SkinCharacteristic.BaseHealth * healthMultiplier).ToString();
                string hps = (data.SkinCharacteristic.RegenerationHealth / data.SkinCharacteristic.RegenearationInterval)
                    .ToString("F1", CultureInfo.InvariantCulture);

                component.TextHealth.text = string.Format(FormatText.HealthStat, health, hps);
            }

            void UpdateDamageStat(WeaponType type)
            {
                WeaponCharacteristicData data = _staticDataService.WeaponCharacteristicData(type);
                int damageMultiplier = _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Damage];

                string damage = (data.WeaponCharacteristic.Damage * damageMultiplier).ToString();
                string dps = (data.WeaponCharacteristic.Damage / data.WeaponCharacteristic.FireInterval)
                    .ToString("F1", CultureInfo.InvariantCulture);

                component.TextDamage.text = string.Format(FormatText.DamageStat, damage, dps);
            }

            _inventoryModel.SelectedSkin
                .Subscribe(UpdateHealthStat)
                .AddTo(component.LifetimeDisposable);

            _inventoryModel.SelectedWeapon
                .Subscribe(UpdateDamageStat)
                .AddTo(component.LifetimeDisposable);

            _progressService.StatsData.Data.Value.Data
                .ObserveEveryValueChanged(data => data[UpgradeButtonType.Health])
                .Subscribe(_ => UpdateHealthStat(_inventoryModel.SelectedSkin.Value))
                .AddTo(component.LifetimeDisposable);
            
            _progressService.StatsData.Data.Value.Data
                .ObserveEveryValueChanged(data => data[UpgradeButtonType.Damage])
                .Subscribe(_ => UpdateDamageStat(_inventoryModel.SelectedWeapon.Value))
                .AddTo(component.LifetimeDisposable);
        }
    }
}
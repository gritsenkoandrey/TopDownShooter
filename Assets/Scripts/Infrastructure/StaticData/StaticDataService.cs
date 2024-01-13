using System.Collections.Generic;
using System.Linq;
using CodeBase.Game.Enums;
using CodeBase.Game.Weapon;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.StaticData
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAssetService _assetService;

        private IDictionary<ZombieType, ZombieData> _monsters;
        private IDictionary<ScreenType, ScreenData> _screens;
        private IDictionary<UpgradeButtonType, UpgradeButtonData> _upgradeButtons;
        private IDictionary<LevelType, LevelData> _levels;
        private IDictionary<WeaponType, WeaponCharacteristicData> _weaponCharacteristics;
        private CharacterData _character;
        private BulletData _bullet;
        private FxData _fxData;
        private TextureArrayData _textureArrayData;
        private UiData _uiData;
        private PoolData _poolData;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        {
            _monsters = _assetService
                .LoadAllFromResources<ZombieData>(AssetAddress.ZombieDataPath)
                .ToDictionary(data => data.ZombieType, data => data);

            _screens = _assetService
                .LoadAllFromResources<ScreenData>(AssetAddress.ScreenDataPath)
                .ToDictionary(data => data.ScreenType, data => data);

            _upgradeButtons = _assetService
                .LoadAllFromResources<UpgradeButtonData>(AssetAddress.UpgradeButtonDataPath)
                .ToDictionary(data => data.UpgradeButtonType, data => data);

            _levels = _assetService
                .LoadAllFromResources<LevelData>(AssetAddress.LevelDataPath)
                .ToDictionary(data => data.LevelType, data => data);

            _weaponCharacteristics = _assetService
                .LoadAllFromResources<WeaponCharacteristicData>(AssetAddress.WeaponCharacteristicDataPath)
                .ToDictionary(data => data.WeaponType, data => data);

            _character = _assetService.LoadFromResources<CharacterData>(AssetAddress.CharacterDataPath);
            _bullet = _assetService.LoadFromResources<BulletData>(AssetAddress.BulletDataPath);
            _fxData = _assetService.LoadFromResources<FxData>(AssetAddress.FxDataPath);
            _textureArrayData = _assetService.LoadFromResources<TextureArrayData>(AssetAddress.TextureArrayDataPath);
            _uiData = _assetService.LoadFromResources<UiData>(AssetAddress.UiDataPath);
            _poolData = _assetService.LoadFromResources<PoolData>(AssetAddress.PoolDataPath);
        }

        ZombieData IStaticDataService.ZombieData(ZombieType type) => 
            _monsters.TryGetValue(type, out ZombieData staticData) ? staticData : null;
        
        ScreenData IStaticDataService.ScreenData(ScreenType type) => 
            _screens.TryGetValue(type, out ScreenData staticData) ? staticData : null;

        UpgradeButtonData IStaticDataService.UpgradeButtonData(UpgradeButtonType type) =>
            _upgradeButtons.TryGetValue(type, out UpgradeButtonData staticData) ? staticData : null;

        LevelData IStaticDataService.LevelData(LevelType type) =>
            _levels.TryGetValue(type, out LevelData staticData) ? staticData : null;

        WeaponCharacteristicData IStaticDataService.WeaponCharacteristicData(WeaponType type) => 
            _weaponCharacteristics.TryGetValue(type, out WeaponCharacteristicData staticData) ? staticData : null;

        CharacterData IStaticDataService.CharacterData() => _character;

        BulletData IStaticDataService.BulletData() => _bullet;

        FxData IStaticDataService.FxData() => _fxData;

        TextureArrayData IStaticDataService.TextureArrayData() => _textureArrayData;

        UiData IStaticDataService.UiData() => _uiData;

        PoolData IStaticDataService.PoolData() => _poolData;
    }
}
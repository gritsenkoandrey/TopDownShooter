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

        private IDictionary<ScreenType, ScreenData> _screens;
        private IDictionary<UpgradeButtonType, UpgradeButtonData> _upgradeButtons;
        private IDictionary<WeaponType, WeaponCharacteristicData> _weaponCharacteristics;
        private IDictionary<ProjectileType, ProjectileData> _projectiles;
        private LevelData _level;
        private CharacterData _character;
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
            _screens = _assetService
                .LoadAllFromResources<ScreenData>(AssetAddress.ScreenDataPath)
                .ToDictionary(data => data.ScreenType, data => data);

            _upgradeButtons = _assetService
                .LoadAllFromResources<UpgradeButtonData>(AssetAddress.UpgradeButtonDataPath)
                .ToDictionary(data => data.UpgradeButtonType, data => data);

            _weaponCharacteristics = _assetService
                .LoadAllFromResources<WeaponCharacteristicData>(AssetAddress.WeaponCharacteristicDataPath)
                .ToDictionary(data => data.WeaponType, data => data);

            _projectiles = _assetService
                .LoadAllFromResources<ProjectileData>(AssetAddress.ProjectileDataPath)
                .ToDictionary(data => data.ProjectileType, data => data);

            _level = _assetService.LoadFromResources<LevelData>(AssetAddress.LevelDataPath);
            _character = _assetService.LoadFromResources<CharacterData>(AssetAddress.CharacterDataPath);
            _fxData = _assetService.LoadFromResources<FxData>(AssetAddress.FxDataPath);
            _textureArrayData = _assetService.LoadFromResources<TextureArrayData>(AssetAddress.TextureArrayDataPath);
            _uiData = _assetService.LoadFromResources<UiData>(AssetAddress.UiDataPath);
            _poolData = _assetService.LoadFromResources<PoolData>(AssetAddress.PoolDataPath);
        }
        
        ScreenData IStaticDataService.ScreenData(ScreenType type) => 
            _screens.TryGetValue(type, out ScreenData staticData) ? staticData : null;

        UpgradeButtonData IStaticDataService.UpgradeButtonData(UpgradeButtonType type) =>
            _upgradeButtons.TryGetValue(type, out UpgradeButtonData staticData) ? staticData : null;

        WeaponCharacteristicData IStaticDataService.WeaponCharacteristicData(WeaponType type) => 
            _weaponCharacteristics.TryGetValue(type, out WeaponCharacteristicData staticData) ? staticData : null;

        public ProjectileData ProjectileData(ProjectileType type) => 
            _projectiles.TryGetValue(type, out var projectileData) ? projectileData : null;

        LevelData IStaticDataService.LevelData() => _level;

        CharacterData IStaticDataService.CharacterData() => _character;

        FxData IStaticDataService.FxData() => _fxData;

        TextureArrayData IStaticDataService.TextureArrayData() => _textureArrayData;

        UiData IStaticDataService.UiData() => _uiData;

        PoolData IStaticDataService.PoolData() => _poolData;
    }
}
using System.Collections.Generic;
using System.Linq;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.DailyTasks;
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
        private IDictionary<EffectType, EffectData> _effects;
        private IDictionary<DailyTaskType, TaskData> _tasks;
        private IDictionary<TurretType, TurretData> _turrets;
        private LevelData _level;
        private CharacterData _character;
        private TextureData _textureData;
        private UiData _uiData;
        private PoolData _poolData;
        private UnitData _unitData;
        private PreviewData _previewData;
        private ShopData _shopData;

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

            _effects = _assetService
                .LoadAllFromResources<EffectData>(AssetAddress.EffectDataPath)
                .ToDictionary(data => data.EffectType, data => data);

            _tasks = _assetService
                .LoadAllFromResources<TaskData>(AssetAddress.TaskDataPath)
                .ToDictionary(data => data.Type, data => data);

            _turrets = _assetService
                .LoadAllFromResources<TurretData>(AssetAddress.TurretDataPath)
                .ToDictionary(data => data.Type, data => data);

            _level = _assetService.LoadFromResources<LevelData>(AssetAddress.LevelDataPath);
            _character = _assetService.LoadFromResources<CharacterData>(AssetAddress.CharacterDataPath);
            _textureData = _assetService.LoadFromResources<TextureData>(AssetAddress.TextureDataPath);
            _uiData = _assetService.LoadFromResources<UiData>(AssetAddress.UiDataPath);
            _poolData = _assetService.LoadFromResources<PoolData>(AssetAddress.PoolDataPath);
            _unitData = _assetService.LoadFromResources<UnitData>(AssetAddress.UnitDataPath);
            _previewData = _assetService.LoadFromResources<PreviewData>(AssetAddress.PreviewDataPath);
            _shopData = _assetService.LoadFromResources<ShopData>(AssetAddress.ShopDataPath);
        }
        
        ScreenData IStaticDataService.ScreenData(ScreenType type) => 
            _screens.TryGetValue(type, out ScreenData staticData) ? staticData : null;

        UpgradeButtonData IStaticDataService.UpgradeButtonData(UpgradeButtonType type) =>
            _upgradeButtons.TryGetValue(type, out UpgradeButtonData staticData) ? staticData : null;

        WeaponCharacteristicData IStaticDataService.WeaponCharacteristicData(WeaponType type) => 
            _weaponCharacteristics.TryGetValue(type, out WeaponCharacteristicData staticData) ? staticData : null;

        ProjectileData IStaticDataService.ProjectileData(ProjectileType type) => 
            _projectiles.TryGetValue(type, out ProjectileData projectileData) ? projectileData : null;

        EffectData IStaticDataService.EffectData(EffectType type) => 
            _effects.TryGetValue(type, out EffectData staticData) ? staticData : null;

        TaskData IStaticDataService.TaskData(DailyTaskType type) => 
            _tasks.TryGetValue(type, out TaskData taskData) ? taskData : null;
        
        TurretData IStaticDataService.TurretData(TurretType type) => 
            _turrets.TryGetValue(type, out TurretData turretData) ? turretData : null;

        LevelData IStaticDataService.LevelData() => _level;
        CharacterData IStaticDataService.CharacterData() => _character;
        TextureData IStaticDataService.TextureArrayData() => _textureData;
        UiData IStaticDataService.UiData() => _uiData;
        PoolData IStaticDataService.PoolData() => _poolData;
        UnitData IStaticDataService.UnitData() => _unitData;
        PreviewData IStaticDataService.PreviewData() => _previewData;
        ShopData IStaticDataService.ShopData() => _shopData;
    }
}
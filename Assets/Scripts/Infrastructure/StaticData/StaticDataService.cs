using System.Collections.Generic;
using System.Linq;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAssetService _assetService;
        
        private const string ZombieDataPath = "StaticData/ZombieData";
        private const string ScreenDataPath = "StaticData/ScreenData";
        private const string CharacterDataPath = "StaticData/CharacterData/CharacterData";
        private const string UpgradeButtonDataPath = "StaticData/UpgradeButtonData";
        private const string BulletPath = "Data/Bulets/Bullet";
        private const string LevelDataPath = "StaticData/LevelData";
        private const string StaticCanvasPath = "Data/Canvas/StaticCanvas";
        private const string FxDataPath = "Data/FX/FxData";

        private Dictionary<ZombieType, ZombieData> _monsters;
        private Dictionary<ScreenType, ScreenData> _screens;
        private Dictionary<UpgradeButtonType, UpgradeButtonData> _upgradeButtons;
        private Dictionary<LevelType, LevelData> _levels;
        private CharacterData _character;
        private CBullet _bullet;
        private StaticCanvas _staticCanvas;
        private FxData _fxData;

        public StaticDataService(IAssetService assetService)
        {
            _assetService = assetService;
        }

        void IStaticDataService.Load()
        {
            _monsters = _assetService
                .LoadAll<ZombieData>(ZombieDataPath)
                .ToDictionary(data => data.ZombieType, data => data);

            _screens = _assetService
                .LoadAll<ScreenData>(ScreenDataPath)
                .ToDictionary(data => data.ScreenType, data => data);

            _upgradeButtons = _assetService
                .LoadAll<UpgradeButtonData>(UpgradeButtonDataPath)
                .ToDictionary(data => data.UpgradeButtonType, data => data);

            _levels = _assetService
                .LoadAll<LevelData>(LevelDataPath)
                .ToDictionary(data => data.LevelType, data => data);

            _character = _assetService.Load<CharacterData>(CharacterDataPath);
            _bullet = _assetService.Load<CBullet>(BulletPath);
            _staticCanvas = _assetService.Load<StaticCanvas>(StaticCanvasPath);
            _fxData = _assetService.Load<FxData>(FxDataPath);
        }

        ZombieData IStaticDataService.ZombieData(ZombieType type) => 
            _monsters.TryGetValue(type, out ZombieData staticData) ? staticData : null;
        
        ScreenData IStaticDataService.ScreenData(ScreenType type) => 
            _screens.TryGetValue(type, out ScreenData staticData) ? staticData : null;

        UpgradeButtonData IStaticDataService.UpgradeButtonData(UpgradeButtonType type) =>
            _upgradeButtons.TryGetValue(type, out UpgradeButtonData staticData) ? staticData : null;

        LevelData IStaticDataService.LevelData(LevelType type) =>
            _levels.TryGetValue(type, out LevelData staticData) ? staticData : null; 

        CharacterData IStaticDataService.CharacterData() => _character;
        FxData IStaticDataService.FxData() => _fxData;
        CBullet IStaticDataService.BulletData() => _bullet;
        StaticCanvas IStaticDataService.StaticCanvasData() => _staticCanvas;
    }
}
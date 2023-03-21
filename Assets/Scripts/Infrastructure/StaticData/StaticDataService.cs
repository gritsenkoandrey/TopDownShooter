using System.Collections.Generic;
using System.Linq;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private readonly IAsset _asset;
        
        private const string ZombieDataPath = "StaticData/ZombieData";
        private const string ScreenDataPath = "StaticData/ScreenData";
        private const string CharacterDataPath = "StaticData/CharacterData/CharacterData";
        private const string UpgradeButtonDataPath = "StaticData/UpgradeButtonData";
        private const string BulletPath = "Data/Bulets/Bullet";
        private const string LevelPath = "Data/Levels/Level";
        private const string StaticCanvasPath = "Data/Canvas/StaticCanvas";

        private Dictionary<ZombieType, ZombieData> _monsters;
        private Dictionary<ScreenType, ScreenData> _screens;
        private Dictionary<UpgradeButtonType, UpgradeButtonData> _upgradeButtons;
        private CharacterData _character;
        private CBullet _bullet;
        private CLevel _level;
        private StaticCanvas _staticCanvas;

        public StaticDataService(IAsset asset)
        {
            _asset = asset;
        }

        public void Load()
        {
            _monsters = Resources
                .LoadAll<ZombieData>(ZombieDataPath)
                .ToDictionary(data => data.ZombieType, data => data);

            _screens = Resources
                .LoadAll<ScreenData>(ScreenDataPath)
                .ToDictionary(data => data.ScreenType, data => data);

            _upgradeButtons = Resources
                .LoadAll<UpgradeButtonData>(UpgradeButtonDataPath)
                .ToDictionary(data => data.UpgradeButtonType, data => data);

            _character = _asset.Load<CharacterData>(CharacterDataPath);
            _bullet = _asset.Load<CBullet>(BulletPath);
            _level = _asset.Load<CLevel>(LevelPath);
            _staticCanvas = _asset.Load<StaticCanvas>(StaticCanvasPath);
        }

        public ZombieData ZombieData(ZombieType type) => 
            _monsters.TryGetValue(type, out ZombieData staticData) ? staticData : null;
        
        public ScreenData ScreenData(ScreenType type) => 
            _screens.TryGetValue(type, out ScreenData staticData) ? staticData : null;

        public UpgradeButtonData UpgradeButtonData(UpgradeButtonType type) =>
            _upgradeButtons.TryGetValue(type, out UpgradeButtonData staticData) ? staticData : null;

        public CharacterData CharacterData() => _character;
        public CLevel LevelData() => _level;
        public CBullet BulletData() => _bullet;
        public StaticCanvas StaticCanvasData() => _staticCanvas;
    }
}
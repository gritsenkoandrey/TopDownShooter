using CodeBase.Game.Enums;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        public void Load();
        public ScreenData ScreenData(ScreenType type);
        public UpgradeButtonData UpgradeButtonData(UpgradeButtonType type);
        public WeaponCharacteristicData WeaponCharacteristicData(WeaponType type);
        public ProjectileData ProjectileData(ProjectileType type);
        public EffectData EffectData(EffectType type);
        public LevelData LevelData();
        public CharacterData CharacterData();
        public TextureData TextureArrayData();
        public UiData UiData();
        public PoolData PoolData();
        public UnitData UnitData();
        public PreviewData PreviewData();
    }
}
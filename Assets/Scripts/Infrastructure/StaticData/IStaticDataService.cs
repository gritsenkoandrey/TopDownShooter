using CodeBase.Game.Enums;
using CodeBase.Game.Weapon;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        public void Load();
        public ScreenData ScreenData(ScreenType type);
        public UpgradeButtonData UpgradeButtonData(UpgradeButtonType type);
        public WeaponCharacteristicData WeaponCharacteristicData(WeaponType type);
        public ProjectileData ProjectileData(ProjectileType type);
        public LevelData LevelData();
        public CharacterData CharacterData();
        public FxData FxData();
        public TextureArrayData TextureArrayData();
        public UiData UiData();
        public PoolData PoolData();
    }
}
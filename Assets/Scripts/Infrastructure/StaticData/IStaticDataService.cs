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
        public ZombieData ZombieData(ZombieType type);
        public ScreenData ScreenData(ScreenType type);
        public UpgradeButtonData UpgradeButtonData(UpgradeButtonType type);
        public LevelData LevelData(LevelType type);
        public WeaponCharacteristicData WeaponCharacteristicData(WeaponType type);
        public CharacterData CharacterData();
        public BulletData BulletData();
        public FxData FxData();
        public TextureArrayData TextureArrayData();
        public UiData UiData();
        public PoolData PoolData();
    }
}
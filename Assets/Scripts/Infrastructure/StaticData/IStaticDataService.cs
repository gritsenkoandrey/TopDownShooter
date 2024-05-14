using CodeBase.Game.Enums;
using CodeBase.Infrastructure.DailyTasks;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        ScreenData ScreenData(ScreenType type);
        UpgradeButtonData UpgradeButtonData(UpgradeButtonType type);
        WeaponCharacteristicData WeaponCharacteristicData(WeaponType type);
        SkinCharacteristicData SkinCharacteristicData(SkinType type);
        ProjectileData ProjectileData(ProjectileType type);
        EffectData EffectData(EffectType type);
        TaskData TaskData(DailyTaskType type);
        TurretData TurretData(TurretType turretType);
        LevelData LevelData();
        CharacterData CharacterData();
        TextureData TextureArrayData();
        UiData UiData();
        PoolData PoolData();
        UnitData UnitData();
        PreviewData PreviewData();
        ShopData ShopData();
    }
}
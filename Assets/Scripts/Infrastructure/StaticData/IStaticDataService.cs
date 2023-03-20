using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        public void Load();
        public ZombieData ZombieData(ZombieType type);
        public ScreenData ScreenData(ScreenType type);
        public UpgradeButtonData UpgradeButtonData(UpgradeButtonType type);
        public CharacterData CharacterData();
    }
}
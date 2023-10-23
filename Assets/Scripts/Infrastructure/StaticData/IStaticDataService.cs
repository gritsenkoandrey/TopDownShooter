using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.StaticData
{
    public interface IStaticDataService : IService
    {
        public UniTask Load();
        public ZombieData ZombieData(ZombieType type);
        public ScreenData ScreenData(ScreenType type);
        public UpgradeButtonData UpgradeButtonData(UpgradeButtonType type);
        public LevelData LevelData(LevelType type);
        public CharacterData CharacterData();
        public BulletData BulletData();
        public FxData FxData();
        public TextureArrayData TextureArrayData();
        public UiData UiData();
    }
}
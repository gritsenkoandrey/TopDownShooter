using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>, ILevel
    {
        public LevelType LevelType { get; private set; }
        public int LevelTime { get; private set; }
        public Entity Entity => this;

        public void SetLevelType(LevelType levelType) => LevelType = levelType;
        public void SetLevelTime(int levelTime) => LevelTime = levelTime;
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>, ILevel
    {
        public int Time { get; private set; }

        public void SetLevelTime(int levelTime) => Time = levelTime;
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>, ILevel, IPause
    {
        public int Time { get; private set; }
        
        private bool _isPaused;

        public void SetTime(int time) => Time = time;

        void ILevel.RemoveTime()
        {
            if (_isPaused) return;

            Time -= 1;
        }

        void IPause.Pause(bool isPause) => _isPaused = isPause;
    }
}
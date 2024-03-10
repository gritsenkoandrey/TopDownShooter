using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UniRx;

namespace CodeBase.Game.Components
{
    public sealed class CLevel : EntityComponent<CLevel>, ILevel, IPause
    {
        public IReactiveProperty<int> Time { get; } = new ReactiveProperty<int>();
        
        private bool _isPaused;
        
        void ILevel.SpendTime()
        {
            if (_isPaused) return;

            Time.Value -= 1;
        }

        void IPause.Pause(bool isPause) => _isPaused = isPause;
    }
}
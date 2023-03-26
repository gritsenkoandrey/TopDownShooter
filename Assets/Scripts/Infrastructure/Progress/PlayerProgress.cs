using CodeBase.Infrastructure.Progress.Data;
using UniRx;

namespace CodeBase.Infrastructure.Progress
{
    [System.Serializable]
    public sealed class PlayerProgress
    {
        public Stats Stats;
        public IntReactiveProperty Money;
        public int Level;

        public PlayerProgress()
        {
            Stats = new Stats();
            Money = new IntReactiveProperty(0);
            Level = 1;
        }
    }
}
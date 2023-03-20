using CodeBase.Infrastructure.Progress.Data;

namespace CodeBase.Infrastructure.Progress
{
    [System.Serializable]
    public sealed class PlayerProgress
    {
        public Stats Stats;
        public int Money;

        public PlayerProgress()
        {
            Stats = new Stats();
            Money = 1000;
        }
    }
}
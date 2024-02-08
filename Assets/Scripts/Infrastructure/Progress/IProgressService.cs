using System;
using CodeBase.Infrastructure.Progress.Data;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        public IData<int> LevelData { get; }
        public IData<int> MoneyData { get; }
        public IData<Stats> StatsData { get; }
        public void Load();
    }
}
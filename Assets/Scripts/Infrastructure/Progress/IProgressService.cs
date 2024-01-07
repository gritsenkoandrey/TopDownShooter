using System;
using CodeBase.Infrastructure.Progress.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IService, IDisposable
    {
        public IData<int> LevelData { get; }
        public IData<int> MoneyData { get; }
        public IData<Stats> StatsData { get; }
        public void Load();
    }
}
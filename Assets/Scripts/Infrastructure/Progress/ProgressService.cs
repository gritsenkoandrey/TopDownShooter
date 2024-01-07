using System;
using System.Collections.Generic;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress.Data;

namespace CodeBase.Infrastructure.Progress
{
    public sealed class ProgressService : IProgressService
    {
        public IData<int> LevelData { get; private set; }
        public IData<int> MoneyData { get; private set; }
        public IData<IDictionary<UpgradeButtonType, int>> StatsData { get; private set; }

        void IProgressService.Load()
        {
            LevelData = new LevelData();
            MoneyData = new MoneyData();
            StatsData = new StatsData();
        }

        void IDisposable.Dispose()
        {
            LevelData?.Dispose();
            MoneyData?.Dispose();
            StatsData?.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IService, IDisposable
    {
        public IData<int> LevelData { get; }
        public IData<int> MoneyData { get; }
        public IData<IDictionary<UpgradeButtonType, int>> StatsData { get; }
        public void Load();
    }
}
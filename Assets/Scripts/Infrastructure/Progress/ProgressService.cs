using System;
using CodeBase.Infrastructure.Progress.Data;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.Progress
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class ProgressService : IProgressService
    {
        public IData<int> LevelData { get; private set; }
        public IData<int> MoneyData { get; private set; }
        public IData<Stats> StatsData { get; private set; }
        public IData<Inventory> InventoryData { get; private set; }

        void IProgressService.Init()
        {
            LevelData = new LevelData();
            MoneyData = new MoneyData();
            StatsData = new StatsData();
            InventoryData = new InventoryData();
        }

        void IDisposable.Dispose()
        {
            LevelData?.Dispose();
            MoneyData?.Dispose();
            StatsData?.Dispose();
            InventoryData?.Dispose();
        }
    }
}
using System;
using CodeBase.Infrastructure.Progress.Data;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        public IData<int> LevelData { get; }
        public IData<int> MoneyData { get; }
        public IData<Stats> StatsData { get; }
        public IData<Inventory> InventoryData { get; }
        public IData<Shop> ShopData { get; }
        public IData<bool> HapticData { get; }
        public void Init();
    }
}
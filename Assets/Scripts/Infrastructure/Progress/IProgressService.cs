using System;
using CodeBase.Infrastructure.Progress.Data;

namespace CodeBase.Infrastructure.Progress
{
    public interface IProgressService : IDisposable
    {
        IData<int> LevelData { get; }
        IData<int> MoneyData { get; }
        IData<Stats> StatsData { get; }
        IData<Inventory> InventoryData { get; }
        IData<Shop> ShopData { get; }
        IData<bool> HapticData { get; }
        IData<DailyTask> DailyTaskData { get; }
        void Init();
    }
}
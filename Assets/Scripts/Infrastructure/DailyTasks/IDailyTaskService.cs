using CodeBase.Game.ComponentsUi;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Infrastructure.DailyTasks
{
    public interface IDailyTaskService
    {
        IReactiveCommand<(DailyTaskType, int)> Update { get; }
        UniTaskVoid Create(CShopTaskProvider provider);
        void Complete(DailyTaskType type);
        int GetRemainingUpdateTime();
    }
}
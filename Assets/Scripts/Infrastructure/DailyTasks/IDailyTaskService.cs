using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.DailyTasks
{
    public interface IDailyTaskService
    {
        IReactiveCommand<(DailyTaskType, int)> Update { get; }
        UniTaskVoid Create(Transform parent);
        void Complete(DailyTaskType type);
        int GetRemainingUpdateTime();
    }
}
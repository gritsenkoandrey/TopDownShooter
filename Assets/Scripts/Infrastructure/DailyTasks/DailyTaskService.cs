using System;
using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.Progress.Data;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.DailyTasks
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class DailyTaskService : IDailyTaskService
    {
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private readonly IStaticDataService _staticDataService;

        public IReactiveCommand<(DailyTaskType, int)> Update { get; } = new ReactiveCommand<(DailyTaskType, int)>();

        private DailyTask DailyTask => _progressService.DailyTaskData.Data.Value;
        private const int Count = 5;

        public DailyTaskService(IProgressService progressService, IUIFactory uiFactory, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _uiFactory = uiFactory;
            _staticDataService = staticDataService;
        }

        async UniTaskVoid IDailyTaskService.Create(CShopTaskProvider provider)
        {
            ClearTaskProvider(provider);

            if (DailyTask.IsNewDay())
            {
                DailyTask.Clear();

                IList<DailyTaskType> types = CreateListTask();

                for (int i = 0; i < Count; i++)
                {
                    DailyTaskType type = types.GetRandomElementAndRemove();
                    TaskData data = _staticDataService.TaskData(type);
                    int level = _progressService.LevelData.Data.Value;
                    Task task = CreateTask(type, data, level);
                    DailyTask.Add(task);
                    
                    await CreateTaskComponent(provider, data, task);
                }
            }
            else
            {
                foreach (Task task in DailyTask.Tasks.Values)
                {
                    TaskData data = _staticDataService.TaskData(task.Type);
                    
                    await CreateTaskComponent(provider, data, task);
                }
            }
        }

        void IDailyTaskService.Complete(DailyTaskType type)
        {
            int reward = DailyTask.Complete(type);

            _progressService.MoneyData.Data.Value += reward;
        }

        int IDailyTaskService.GetRemainingUpdateTime() => 
            (int)(DateTime.UtcNow.Date + TimeSpan.FromDays(1) - DateTime.UtcNow).TotalSeconds;

        private Task CreateTask(DailyTaskType type, TaskData data, int level)
        {
            int maxScore = data.MaxScore + Mathf.RoundToInt(level * data.Multiplier * data.MaxScore);
            int reward = data.Reward + Mathf.RoundToInt(level * data.Multiplier * data.MaxScore);

            return new Task(type, reward, maxScore);
        }

        private List<DailyTaskType> CreateListTask() => 
            EnumExtension.GenerateEnumList<DailyTaskType>(type => type != DailyTaskType.None);

        private void ClearTaskProvider(CShopTaskProvider provider)
        {
            if (provider.Tasks.IsReadOnly)
            {
                provider.Tasks = new List<CTask>(Count);
            }
            else
            {
                foreach (CTask task in provider.Tasks)
                {
                    UnityEngine.Object.Destroy(task.gameObject);
                }
                
                provider.Tasks.Clear();
            }
        }
        
        private async UniTask CreateTaskComponent(CShopTaskProvider provider, TaskData data, Task task)
        {
            CTask prefab = await _uiFactory.CreateDailyTask(provider.Content);
            provider.Tasks.Add(prefab);
            prefab.QuestText.text = data.Text;
            prefab.ButtonText.text = string.Format(FormatText.TaskGetButton, task.Reward.Trim());
            prefab.Task.Value = task;
        }
    }
}
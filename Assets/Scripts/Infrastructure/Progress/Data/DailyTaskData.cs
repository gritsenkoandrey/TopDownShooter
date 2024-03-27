using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.DailyTasks;
using CodeBase.Infrastructure.SaveLoad;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class DailyTaskData : ISaveLoad<DailyTask>
    {
        public IReactiveProperty<DailyTask> Data { get; }

        public DailyTaskData()
        {
            Data = new ReactiveProperty<DailyTask>(Load());
            Data.Value.Save += Save;
        }

        public void Save(DailyTask data)
        {
            PlayerPrefs.SetString(DataKeys.DailyTask, data.ToSerialize());
            PlayerPrefs.Save();
        }

        public DailyTask Load()
        {
            return PlayerPrefs.HasKey(DataKeys.DailyTask)
                ? PlayerPrefs.GetString(DataKeys.DailyTask)?.ToDeserialize<DailyTask>()
                : SetDefaultValue();
        }

        private DailyTask SetDefaultValue() => new(DateTime.MinValue.Date, new Dictionary<DailyTaskType, Task>());

        public void Dispose() => Data.Value.Save -= Save;
    }

    [JsonObject]
    public sealed class DailyTask
    {
        [JsonProperty] private DateTime _enterDate;
        [JsonProperty] private Dictionary<DailyTaskType, Task> _tasks;

        [JsonIgnore] public IReadOnlyDictionary<DailyTaskType, Task> Tasks => _tasks;

        public event Action<DailyTask> Save;

        public DailyTask(DateTime enterDate, Dictionary<DailyTaskType, Task> tasks)
        {
            _enterDate = enterDate;
            _tasks = tasks;
        }

        public void Add(Task task)
        {
            if (Contains(task.Type) == false)
            {
                _tasks.Add(task.Type, task);
                
                Save?.Invoke(this);
            }
        }

        public void Update(DailyTaskType type, int score)
        {
            if (Contains(type))
            {
                _tasks[type].Score += score;
                
                Save?.Invoke(this);
            }
        }

        public int Complete(DailyTaskType type)
        {
            if (Contains(type))
            {
                _tasks[type].IsDone = true;
                
                Save?.Invoke(this);

                return _tasks[type].Reward;
            }

            return default;
        }

        public void Clear()
        {
            _enterDate = DateTime.UtcNow;
            _tasks.Clear();

            Save?.Invoke(this);
        }

        public bool IsNewDay()
        {
            if (_enterDate.Date.Date.Equals(DateTime.UtcNow.Date))
            {
                return false;
            }

            return true;
        }

        private bool Contains(DailyTaskType type) => _tasks.ContainsKey(type);
    }
}
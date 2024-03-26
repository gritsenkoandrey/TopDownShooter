using CodeBase.Infrastructure.DailyTasks;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(TaskData), menuName = "Data/" + nameof(TaskData))]
    public sealed class TaskData : ScriptableObject
    {
        public DailyTaskType Type;
        public int Reward;
        public int MaxScore;
        public string Text;
        [Range(0f, 5f)] public float Multiplier;
    }
}
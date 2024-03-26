namespace CodeBase.Infrastructure.DailyTasks
{
    [System.Serializable]
    public sealed class Task
    {
        public readonly DailyTaskType Type;
        public readonly int Reward;
        public readonly int MaxScore;
        public int Score;
        public bool IsDone;

        public Task(DailyTaskType type, int reward, int maxScore, int score = 0, bool isDone = false)
        {
            Type = type;
            Reward = reward;
            MaxScore = maxScore;
            Score = score;
            IsDone = isDone;
        }
    }
}
namespace CodeBase.Infrastructure.DailyTasks
{
    [System.Serializable]
    public enum DailyTaskType : byte
    {
        EarnMoney              = 0,
        SpendMoney             = 1,
        KillEnemyRangeWeapon   = 2,
        KillEnemyMeleeWeapon   = 3,
        CompleteLevel          = 4,
        CompleteLevelThreeStar = 5,
        DealDamage             = 6,
        PlayMinutes            = 7,
        EnterGame              = 8,
        
        None = byte.MaxValue
    }
}
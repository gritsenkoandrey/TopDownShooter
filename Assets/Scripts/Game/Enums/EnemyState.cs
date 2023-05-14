namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum EnemyState : byte
    {
        None    = byte.MaxValue,
        Idle    = 0,
        Patrol  = 1,
        Pursuit = 2,
    }
}
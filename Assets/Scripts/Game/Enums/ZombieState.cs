namespace CodeBase.Game.Enums
{
    [System.Serializable]
    public enum ZombieState : byte
    {
        None    = byte.MaxValue,
        Idle    = 0,
        Patrol  = 1,
        Pursuit = 2,
    }
}
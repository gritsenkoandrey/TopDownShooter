namespace AndreyGritsenko.Game.StateMachine
{
    [System.Serializable]
    public enum State : byte
    {
        None    = byte.MaxValue,
        Idle    = 0,
        Patrol  = 1,
        Pursuit = 2,
    }
}
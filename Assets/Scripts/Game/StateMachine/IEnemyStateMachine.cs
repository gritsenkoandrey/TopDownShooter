namespace CodeBase.Game.StateMachine
{
    public interface IEnemyStateMachine
    {
        public void Enter<T>() where T : class, IEnemyState;
        public void Tick();
    }
}
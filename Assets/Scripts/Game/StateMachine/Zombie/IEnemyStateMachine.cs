namespace CodeBase.Game.StateMachine.Zombie
{
    public interface IEnemyStateMachine
    {
        public void Enter<T>() where T : class, IEnemyState;
        public void Tick();
    }
}
namespace CodeBase.Game.StateMachine
{
    public interface IStateMachine
    {
        public void Enter<T>() where T : class, IState;
        public void Tick();
    }
}
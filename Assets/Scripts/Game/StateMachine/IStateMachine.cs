namespace CodeBase.Game.StateMachine
{
    public interface IStateMachine
    {
        void Enter<T>() where T : class, IState;
        void Tick();
    }
}
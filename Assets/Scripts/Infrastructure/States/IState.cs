namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitState
    {
        public void Enter();
    }
}
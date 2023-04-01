namespace CodeBase.Infrastructure.States
{
    public interface IEnterState : IExitState
    {
        public void Enter();
    }
}
namespace CodeBase.Infrastructure.States
{
    public interface IEnterState : IExitState
    {
        void Enter();
    }
}
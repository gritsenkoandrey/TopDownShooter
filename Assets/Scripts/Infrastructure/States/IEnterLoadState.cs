namespace CodeBase.Infrastructure.States
{
    public interface IEnterLoadState<in TLoad> : IExitState
    {
        void Enter(TLoad load);
    }
}
namespace CodeBase.Infrastructure.States
{
    public interface IEnterLoadState<in TLoad> : IExitState
    {
        public void Enter(TLoad load);
    }
}
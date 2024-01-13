using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public interface IGameStateService : IService
    {
        void Enter<TState>() where TState : class, IEnterState;
        void Enter<TState, TLoad>(TLoad load) where TState : class, IEnterLoadState<TLoad>;
        void AddState<TState>(TState state) where TState : class, IExitState;
    }
}
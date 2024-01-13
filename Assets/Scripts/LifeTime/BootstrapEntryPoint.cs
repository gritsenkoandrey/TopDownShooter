using CodeBase.Infrastructure.States;
using JetBrains.Annotations;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class BootstrapEntryPoint : IStartable
    {
        private readonly IGameStateService _gameStateService;

        public BootstrapEntryPoint(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
        void IStartable.Start()
        {
            _gameStateService.Enter<StateBootstrap>();
        }
    }
}
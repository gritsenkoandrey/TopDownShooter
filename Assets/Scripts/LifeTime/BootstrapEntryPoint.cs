using CodeBase.Infrastructure.Factories.StateMachine;
using CodeBase.Infrastructure.States;
using JetBrains.Annotations;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class BootstrapEntryPoint : IStartable
    {
        private readonly IStateMachineFactory _stateMachineFactory;

        public BootstrapEntryPoint(IStateMachineFactory stateMachineFactory)
        {
            _stateMachineFactory = stateMachineFactory;
        }
        
        void IStartable.Start()
        {
            IGameStateMachine stateMachine = _stateMachineFactory.CreateGameStateMachine();
            
            stateMachine.Enter<StateBootstrap>();
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.StateMachine;

namespace CodeBase.Game.Components
{
    public sealed class CStateMachine : EntityComponent<CStateMachine>
    {
        public bool IsCreated { get; private set; }
        public IStateMachine StateMachine { get; private set; }
        
        public void CreateStateMachine(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            IsCreated = true;
        }
    }
}
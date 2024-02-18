using CodeBase.ECSCore;
using CodeBase.Game.StateMachine;

namespace CodeBase.Game.Components
{
    public sealed class CStateMachine : EntityComponent<CStateMachine>
    {
        public IStateMachine StateMachine { get; private set; }

        private bool _isCreated;
        
        public void CreateStateMachine(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            _isCreated = true;
        }

        public void Execute()
        {
            if (_isCreated == false) return;

            StateMachine.Tick();
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine;

namespace CodeBase.Game.Components
{
    public sealed class CStateMachine : EntityComponent<CStateMachine>, IPause
    {
        public IStateMachine StateMachine { get; private set; }

        private bool _isExecute;
        
        public void CreateStateMachine(IStateMachine stateMachine)
        {
            StateMachine = stateMachine;
            
            _isExecute = true;
        }

        public void Execute()
        {
            if (_isExecute == false) return;

            StateMachine.Tick();
        }

        void IPause.Pause(bool isPause) => _isExecute = !isPause;
    }
}
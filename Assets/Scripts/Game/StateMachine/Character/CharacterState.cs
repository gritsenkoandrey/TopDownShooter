using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Character
{
    public abstract class CharacterState
    {
        private readonly IStateMachine _stateMachine;
        
        private protected CCharacter Character { get; }

        private protected CharacterState(IStateMachine stateMachine, CCharacter character)
        {
            _stateMachine = stateMachine;
            Character = character;
        }

        protected void EnterState<T>() where T : CharacterState, IState => _stateMachine.Enter<T>();
    }
}
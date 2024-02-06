using CodeBase.Game.Components;

namespace CodeBase.Game.StateMachine.Character
{
    public abstract class CharacterState
    {
        private protected readonly IStateMachine StateMachine;
        private protected readonly CCharacter Character;
        
        private protected const float MinInputMagnitude = 0.1f;

        private protected CharacterState(IStateMachine stateMachine, CCharacter character)
        {
            StateMachine = stateMachine;
            Character = character;
        }
    }
}
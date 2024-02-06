using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        public IGameStateMachine CreateGameStateMachine();
        public IStateMachine CreateCharacterStateMachine(CCharacter character);
        public IStateMachine CreateUnitStateMachine(CUnit unit);
    }
}
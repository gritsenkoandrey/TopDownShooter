using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;

namespace CodeBase.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        public IStateMachine CreateCharacterStateMachine(CCharacter character);
        public IStateMachine CreateUnitStateMachine(CUnit unit);
    }
}
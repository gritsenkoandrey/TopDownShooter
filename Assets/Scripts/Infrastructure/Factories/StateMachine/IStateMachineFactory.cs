using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure.Factories.StateMachine
{
    public interface IStateMachineFactory
    {
        IGameStateMachine CreateGameStateMachine();
        IStateMachine CreateCharacterStateMachine(CCharacter character);
        IStateMachine CreateUnitStateMachine(CUnit unit);
        IStateMachine CreateTurretStateMachine(CTurret turret);
    }
}
using CodeBase.Game.Components;

namespace CodeBase.Game.Interfaces
{
    public interface IStateMachineComponent
    {
        CStateMachine StateMachine { get; }
    }
}
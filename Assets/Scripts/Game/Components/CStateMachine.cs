using CodeBase.ECSCore;
using CodeBase.Game.StateMachine;
using UniRx;

namespace CodeBase.Game.Components
{
    public sealed class CStateMachine : EntityComponent<CStateMachine>
    {
        public IStateMachine StateMachine { get; set; }
        public ReactiveCommand UpdateStateMachine { get; } = new();
    }
}
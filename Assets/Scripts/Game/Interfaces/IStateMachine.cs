using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface IStateMachine
    {
        public ReactiveCommand UpdateStateMachine { get; }
    }
}
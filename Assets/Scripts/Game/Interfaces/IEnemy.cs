using CodeBase.Game.StateMachine.Zombie;
using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy : IPosition
    {
        public IEnemyStateMachine StateMachine { get; }
        public ReactiveCommand<int> DamageReceived { get; }
    }
}
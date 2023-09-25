using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.StaticData.Data;
using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy : ITarget
    {
        public EnemyStats Stats { get; }
        public IEnemyStateMachine StateMachine { get; }
        public ReactiveProperty<ITarget> Target { get; }
    }
}
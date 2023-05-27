using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.StaticData.Data;
using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy : IPosition
    {
        public CHealth Health { get; }
        public ZombieStats Stats { get; }
        public IEnemyStateMachine StateMachine { get; }
        public ReactiveCommand<int> DamageReceived { get; }
    }
}
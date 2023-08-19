using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.StaticData.Data;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy : ITarget
    {
        public EnemyStats Stats { get; }
        public IEnemyStateMachine StateMachine { get; }
    }
}
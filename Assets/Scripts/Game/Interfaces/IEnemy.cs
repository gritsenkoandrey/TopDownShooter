using CodeBase.Game.Components;
using CodeBase.Infrastructure.StaticData.Data;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy : ITarget
    {
        public EnemyStats Stats { get; }
        public CStateMachine StateMachine { get; }
    }
}
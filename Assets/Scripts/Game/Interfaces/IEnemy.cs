using CodeBase.Game.Components;
using CodeBase.Infrastructure.StaticData.Data;
using UniRx;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy : ITarget
    {
        public EnemyStats Stats { get; }
        public CStateMachine StateMachine { get; }
        public ReactiveProperty<ICharacter> Target { get; }
    }
}
using CodeBase.Game.StateMachine;
using CodeBase.Game.StateMachine.Zombie;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy
    {
        public IEnemyStateMachine StateMachine { get; }
        public Vector3 Position { get; }
        public ReactiveCommand<int> DamageReceived { get; }
    }
}
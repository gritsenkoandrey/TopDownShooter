using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy
    {
        public NavMeshAgent Agent { get; }
        public CRadar Radar { get; }
        public ZombieStateMachine StateMachine { get; }
        public Vector3 Position { get; }
        public ReactiveCommand<int> DamageReceived { get; }
    }
}
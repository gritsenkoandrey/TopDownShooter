using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Interfaces
{
    public interface IEnemy
    {
        public NavMeshAgent Agent { get; }
        public CRadar Radar { get; }
        public EnemyState State { get; set; }
        public Vector3 Position { get; }
        public ReactiveCommand<int> DamageReceived { get; }
    }
}
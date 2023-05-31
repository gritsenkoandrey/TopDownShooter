using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateIdle : ZombieState, IEnemyState
    {
        private float _delay;
        private readonly float _aggroRadius;

        public ZombieStateIdle(IEnemyStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie)
        {
            _aggroRadius = zombie.Stats.AggroRadius * zombie.Stats.AggroRadius;
        }

        void IEnemyState.Enter()
        {
            _delay = Zombie.Stats.StayDelay;
            Zombie.Radar.Draw.Execute();
        }

        void IEnemyState.Exit() { }

        void IEnemyState.Tick()
        {
            if (Distance() < _aggroRadius || Zombie.IsAggro)
            {
                StateMachine.Enter<ZombieStatePursuit>();
            }
            else
            {
                if (_delay > 0f)
                {
                    _delay -= Time.deltaTime;
                }
                else
                {
                    StateMachine.Enter<ZombieStatePatrol>();
                }
            }
        }

        private float Distance() => (Zombie.Target.Value.Position - Zombie.Position).sqrMagnitude;
    }
}
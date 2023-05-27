using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateIdle : ZombieState, IEnemyState
    {
        private float _delay;

        public ZombieStateIdle(IEnemyStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie) { }

        void IEnemyState.Enter()
        {
            _delay = Zombie.Stats.StayDelay;
            Zombie.Radar.Radius = Zombie.Stats.AggroRadius;
            Zombie.Radar.Draw.Execute();
        }

        void IEnemyState.Exit() { }

        void IEnemyState.Tick()
        {
            if (_delay > 0f)
            {
                _delay -= Time.deltaTime;
            }
            else
            {
                if (Distance() < Zombie.Radar.Radius || Zombie.IsAggro)
                {
                    StateMachine.Enter<ZombieStatePursuit>();
                }
                else
                {
                    StateMachine.Enter<ZombieStatePatrol>();
                }
            }
        }

        private float Distance() => Vector3.Distance(Zombie.Position, Zombie.Target.Position);
    }
}
using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateIdle : ZombieState
    {
        private readonly CZombie _zombie;
        
        private float _delay;
        
        public ZombieStateIdle(ZombieStateMachine stateMachine, CZombie zombie) : base(stateMachine)
        {
            _zombie = zombie;
        }

        public override void Enter()
        {
            base.Enter();

            _delay = _zombie.Stats.StayDelay;
             
            _zombie.Radar.Radius = _zombie.Stats.AggroRadius;
            _zombie.Radar.Draw.Execute();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Tick()
        {
            base.Tick();
            
            if (_delay > 0f)
            {
                _delay -= Time.deltaTime;
            }
            else
            {
                if (Distance() < _zombie.Radar.Radius || _zombie.IsAggro)
                {
                    StateMachine.Enter<ZombieStatePursuit>();
                }
                else
                {
                    StateMachine.Enter<ZombieStatePatrol>();
                }
            }
        }
        
        private float Distance() => Vector3.Distance(_zombie.Position, _zombie.Character.Position);
    }
}
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateIdle : ZombieState, IState
    {
        private float _aggroRadius;
        private float _delay;
        private int _startHealth;

        public ZombieStateIdle(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel) { }

        void IState.Enter()
        {
            _aggroRadius = Mathf.Pow(Zombie.Stats.AggroRadius, 2);
            _startHealth = Zombie.Health.CurrentHealth.Value;
            _delay = Zombie.Stats.StayDelay;
            Zombie.Animator.OnIdle.Execute();
            Zombie.Radar.Draw.Execute();
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            if (DistanceToTarget() < _aggroRadius || IsAggro())
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

        private float DistanceToTarget() => (LevelModel.Character.Position - Zombie.Position).sqrMagnitude;

        private bool IsAggro() => _startHealth > Zombie.Health.CurrentHealth.Value;
    }
}
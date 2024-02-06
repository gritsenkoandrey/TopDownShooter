using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Unit
{
    public sealed class UnitStateIdle : UnitState, IState
    {
        private LevelModel _levelModel;

        private float _detectionDistance;
        private float _delay;
        private int _startHealth;

        public UnitStateIdle(IStateMachine stateMachine, CUnit unit) : base(stateMachine, unit)
        {
        }
        
        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            _detectionDistance = Mathf.Pow(Unit.WeaponMediator.CurrentWeapon.Weapon.DetectionDistance(), 2);
            _delay = Unit.UnitStats.StayDelay;
            _startHealth = Unit.Health.CurrentHealth.Value;
            Unit.Animator.OnRun.Execute(0f);
            Unit.Radar.Draw.Execute();
        }

        public void Exit() { }

        public void Tick()
        {
            if (Unit.Health.IsAlive == false)
            {
                StateMachine.Enter<UnitStateDeath>();
                
                return;
            }

            if (DistanceToTarget() < _detectionDistance || IsAggro())
            {
                StateMachine.Enter<UnitStatePursuit>();
            }
            else
            {
                if (_delay > 0f)
                {
                    _delay -= Time.deltaTime;
                }
                else
                {
                    StateMachine.Enter<UnitStatePatrol>();
                }
            }
        }
        
        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;

        private bool IsAggro() => _startHealth > Unit.Health.CurrentHealth.Value;
    }
}
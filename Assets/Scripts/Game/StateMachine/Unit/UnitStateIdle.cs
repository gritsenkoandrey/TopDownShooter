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
        }

        public void Exit() { }

        public void Tick()
        {
            if (IsDeath())
            {
                EnterState<UnitStateDeath>();
                
                return;
            }

            if (CanPursuit())
            {
                EnterState<UnitStatePursuit>();
            }
            else
            {
                if (_delay > 0f)
                {
                    _delay -= Time.deltaTime;
                }
                else
                {
                    EnterState<UnitStatePatrol>();
                }
            }
        }


        private float DistanceToTarget() => (_levelModel.Character.Position - Unit.Position).sqrMagnitude;
        private bool IsAggro() => _startHealth > Unit.Health.CurrentHealth.Value;
        private bool CanPursuit() => DistanceToTarget() < _detectionDistance || IsAggro();
        private bool IsDeath() => Unit.Health.IsAlive == false;
    }
}
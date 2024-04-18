using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.StateMachine.Turret
{
    public sealed class TurretStateIdle : TurretState, IState
    {
        private LevelModel _levelModel;

        private Vector3 _direction;
        
        private const float Speed = 10f;

        public TurretStateIdle(IStateMachine stateMachine, CTurret turret) : base(stateMachine, turret)
        {
        }

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        public void Enter()
        {
            Turret.Radar.Draw.Execute();
            
            _direction = Random.Range(-1f, 1f) > 0f ? Vector3.up : Vector3.down;
        }

        public void Exit()
        {
            Turret.Radar.Clear.Execute();
        }

        public void Tick()
        {
            if (Turret.Health.IsAlive == false)
            {
                EnterState<TurretStateDeath>();
                
                return;
            }

            if (DistanceToTarget() < Turret.Weapon.Weapon.AttackDistance())
            {
                EnterState<TurretStateFight>();
                
                return;
            }
            
            Rotation();
        }

        private void Rotation() => Turret.Rotate.localEulerAngles += _direction * Time.deltaTime * Speed;
        private float DistanceToTarget() => (_levelModel.Character.Position - Turret.Position).sqrMagnitude;
    }
}
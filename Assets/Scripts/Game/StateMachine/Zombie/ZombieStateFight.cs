using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Models;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateFight : ZombieState, IState
    {
        private float _minDistanceToTarget;
        private bool _canAttack;
        private ICharacter _target;

        public ZombieStateFight(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel)
        {
            _canAttack = true;
        }

        void IState.Enter()
        {
            _minDistanceToTarget = Mathf.Pow(Zombie.Stats.MinDistanceToTarget, 2);
            _target = LevelModel.Character;
            Zombie.Animator.OnIdle.Execute();
        }

        void IState.Exit()
        {
            _target = null;
        }

        void IState.Tick()
        {
            if (DistanceToTarget() > _minDistanceToTarget && _target.Health.IsAlive)
            {
                StateMachine.Enter<ZombieStatePursuit>();
                
                return;
            }
            
            if (_canAttack)
            {
                SetCanAttack();
                
                Zombie.Attack.Execute();
                Zombie.Animator.OnAttack.Execute();
            }
        }

        private void SetCanAttack()
        {
            _canAttack = false;
            
            DOVirtual.DelayedCall(Zombie.Stats.AttackDelay, () => _canAttack = true);
        }
        
        private float DistanceToTarget() => (_target.Move.Position - Zombie.Position).sqrMagnitude;
    }
}
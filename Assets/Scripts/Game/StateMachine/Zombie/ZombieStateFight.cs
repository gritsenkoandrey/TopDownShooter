using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateFight : ZombieState, IState
    {
        private float _minDistanceToTarget;
        private bool _canAttack;

        public ZombieStateFight(IStateMachine stateMachine, CZombie zombie, LevelModel levelModel) 
            : base(stateMachine, zombie, levelModel)
        {
            _canAttack = true;
        }

        void IState.Enter()
        {
            _minDistanceToTarget = Mathf.Pow(Zombie.Stats.MinDistanceToTarget, 2);
            Zombie.Animator.OnIdle.Execute();
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            if (DistanceToTarget() > _minDistanceToTarget && LevelModel.Character.Health.IsAlive)
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
        
        private float DistanceToTarget() => (LevelModel.Character.Move.Position - Zombie.Position).sqrMagnitude;
    }
}
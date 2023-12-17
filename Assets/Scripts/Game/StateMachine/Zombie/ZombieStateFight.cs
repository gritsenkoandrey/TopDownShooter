using CodeBase.Game.Components;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStateFight : ZombieState, IState
    {
        private readonly float _minDistanceToTarget;
        
        private bool _canAttack;

        public ZombieStateFight(IStateMachine stateMachine, CZombie zombie) : base(stateMachine, zombie)
        {
            _canAttack = true;
            _minDistanceToTarget = Mathf.Pow(zombie.Stats.MinDistanceToTarget, 2);
        }

        void IState.Enter()
        {
            Zombie.Animator.OnIdle.Execute();

            if (_canAttack)
            {
                SetCanAttack();
                
                Zombie.Attack.Execute();
                Zombie.Animator.OnAttack.Execute();
            }
        }

        void IState.Exit() { }

        void IState.Tick()
        {
            if (DistanceToTarget() > _minDistanceToTarget && Zombie.Target.Value.Health.IsAlive)
            {
                StateMachine.Enter<ZombieStatePursuit>();
            }
        }

        private void SetCanAttack()
        {
            _canAttack = false;
            
            DOVirtual.DelayedCall(Zombie.Stats.AttackDelay, () => _canAttack = true);
        }
        
        private float DistanceToTarget() => (Zombie.Target.Value.Move.Position - Zombie.Position).sqrMagnitude;
    }
}
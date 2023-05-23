using CodeBase.Game.Components;
using CodeBase.Utils;
using UnityEngine;

namespace CodeBase.Game.StateMachine.Zombie
{
    public sealed class ZombieStatePursuit : ZombieState
    {
        private readonly CZombie _zombie;

        private float _attackDelay;

        public ZombieStatePursuit(ZombieStateMachine stateMachine, CZombie zombie) : base(stateMachine)
        {
            _zombie = zombie;
        }
        
        public override void Enter()
        {
            base.Enter();

            _attackDelay = _zombie.Stats.AttackDelay;
            _zombie.Agent.speed = _zombie.Stats.RunSpeed;
            _zombie.Animator.Animator.SetFloat(Animations.RunBlend, 1f);
            _zombie.Radar.Clear.Execute();
        }

        public override void Exit()
        {
            base.Exit();
            
            _zombie.Agent.ResetPath();
            _attackDelay = _zombie.Stats.AttackDelay;
        }

        public override void Tick()
        {
            base.Tick();
            
            if (Distance() > _zombie.Stats.PursuitRadius)
            {
                _zombie.IsAggro = false;
                
                StateMachine.Enter<ZombieStateIdle>();
            }
            else
            {
                LockAt();
                
                if (Distance() < _zombie.Stats.MinDistanceToTarget)
                {
                    if (_zombie.Agent.hasPath)
                    {
                        _zombie.Agent.ResetPath();
                    }
                    
                    Attack();
                }
                else
                {
                    _zombie.Agent.SetDestination(_zombie.Character.Position);
                }

                _attackDelay -= Time.deltaTime;
            }
        }
        
        private void Attack()
        {
            if (_attackDelay < 0f)
            {
                _attackDelay = _zombie.Stats.AttackDelay;

                _zombie.Melee.Attack.Execute();
            }
        }
        
        private void LockAt()
        {
            Quaternion lookRotation = Quaternion.LookRotation(_zombie.Character.Position - _zombie.Position);

            _zombie.transform.rotation = Quaternion.Slerp(_zombie.transform.rotation, lookRotation, 0.5f);
        }
        
        private float Distance() => Vector3.Distance(_zombie.Position, _zombie.Character.Position);
    }
}
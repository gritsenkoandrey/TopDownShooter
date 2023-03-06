using AndreyGritsenko.ECSCore;
using AndreyGritsenko.Game.Components;
using AndreyGritsenko.Game.StateMachine;
using UniRx;

namespace AndreyGritsenko.Game.Systems
{
    public sealed class SEnemyMovement : SystemComponent<CEnemy>
    {
        private readonly CCharacter _character;

        public SEnemyMovement(CCharacter character)
        {
            _character = character;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            EnemyStateMachine enemyStateMachine = new EnemyStateMachine(component, component.Radar, _character);
            
            enemyStateMachine.Init();

            Observable.EveryUpdate()
                .Subscribe(_ => { enemyStateMachine.Tick(); })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}
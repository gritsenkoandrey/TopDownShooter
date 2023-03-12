using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyStateMachine : SystemComponent<CEnemy>
    {
        private readonly CCharacter _character;

        public SEnemyStateMachine(CCharacter character)
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

        protected override void OnTick()
        {
            base.OnTick();
            
            foreach (CEnemy entity in Entities)
            {
                entity.UpdateStateMachine.Execute();
            }
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);

            EnemyStateMachine enemyStateMachine = new EnemyStateMachine(component, _character);
            
            enemyStateMachine.Init();

            component.UpdateStateMachine
                .Subscribe(_ => enemyStateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}
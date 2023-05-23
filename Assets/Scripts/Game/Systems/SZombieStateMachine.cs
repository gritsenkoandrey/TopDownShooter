using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieStateMachine : SystemComponent<CZombie>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            
            foreach (CZombie entity in Entities)
            {
                entity.UpdateStateMachine.Execute();
            }
        }

        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            InitializeStateMachine(component);
        }

        protected override void OnDisableComponent(CZombie component)
        {
            base.OnDisableComponent(component);
        }

        private void InitializeStateMachine(CZombie component)
        {
            component.StateMachine = new ZombieStateMachine(component);
            
            component.StateMachine.Enter<ZombieStateIdle>();

            component.UpdateStateMachine
                .Subscribe(_ => component.StateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}
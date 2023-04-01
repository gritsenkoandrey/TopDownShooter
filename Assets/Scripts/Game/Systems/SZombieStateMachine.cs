using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using Cysharp.Threading.Tasks;
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

        private async void InitializeStateMachine(CZombie component)
        {
            await UniTask.NextFrame();
            
            ZombieStateMachine enemyStateMachine = new ZombieStateMachine(component);
            
            enemyStateMachine.Init();

            component.UpdateStateMachine
                .Subscribe(_ => enemyStateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}
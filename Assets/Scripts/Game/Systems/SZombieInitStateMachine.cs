using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Models;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieInitStateMachine : SystemComponent<CZombie>
    {
        private readonly LevelModel _levelModel;

        public SZombieInitStateMachine(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            InitializeStateMachine(component);
        }

        private void InitializeStateMachine(CZombie component)
        {
            // await UniTask.Delay(TimeSpan.FromSeconds(1f));
            
            component.StateMachine.SetStateMachine(new ZombieStateMachine(component, _levelModel));

            component.StateMachine.UpdateStateMachine
                .Subscribe(_ => component.StateMachine.StateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}
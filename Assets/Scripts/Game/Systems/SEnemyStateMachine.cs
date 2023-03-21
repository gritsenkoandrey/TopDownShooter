using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine;
using CodeBase.Infrastructure.Factories.Game;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyStateMachine : SystemComponent<CEnemy>
    {
        private readonly IGameFactory _gameFactory;

        public SEnemyStateMachine(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
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

            InitializeStateMachine(component);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }

        private async void InitializeStateMachine(CEnemy component)
        {
            await UniTask.DelayFrame(1);
            
            EnemyStateMachine enemyStateMachine = new EnemyStateMachine(component, _gameFactory.CurrentCharacter);
            
            enemyStateMachine.Init();

            component.UpdateStateMachine
                .Subscribe(_ => enemyStateMachine.Tick())
                .AddTo(component.LifetimeDisposable);
        }
    }
}
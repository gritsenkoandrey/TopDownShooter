using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyDeath : SystemComponent<CEnemy>
    {
        private IGameFactory _gameFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);
            
            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .ObserveOnMainThread()
                .Subscribe(health =>
                {
                    if (health <= 0)
                    {
                        component.Agent.ResetPath();
                        component.Radar.Clear.Execute();

                        _gameFactory.CurrentCharacter.Enemies.Remove(component);
                        
                        component.LifetimeDisposable.Clear();
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}
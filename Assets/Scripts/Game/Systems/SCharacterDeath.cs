using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.States;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterDeath : SystemComponent<CCharacter>
    {
        private readonly IGameStateService _gameStateService;

        public SCharacterDeath(IGameStateService gameStateService)
        {
            _gameStateService = gameStateService;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .Subscribe(health =>
                {
                    if (health <= 0)
                    {
                        _gameStateService.Enter<StateFail>();
                        
                        component.LifetimeDisposable.Clear();

                        foreach (IEnemy enemy in component.Enemies)
                        {
                            enemy.Agent.ResetPath();
                            enemy.Radar.Clear.Execute();
                            enemy.State = EnemyState.None;
                        }
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CCharacter component)
        {
            base.OnDisableComponent(component);
        }
    }
}
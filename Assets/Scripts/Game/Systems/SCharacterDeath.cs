using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Zombie;
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
                .Subscribe(_ =>
                {
                    if (!component.Health.IsAlive)
                    {
                        _gameStateService.Enter<StateFail>();
                        
                        component.LifetimeDisposable.Clear();

                        foreach (IEnemy enemy in component.Enemies)
                        {
                            enemy.StateMachine.Enter<ZombieStateNone>();
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
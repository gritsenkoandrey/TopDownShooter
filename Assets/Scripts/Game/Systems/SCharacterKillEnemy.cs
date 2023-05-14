using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.States;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterKillEnemy : SystemComponent<CCharacter>
    {
        private readonly IGameStateService _gameStateService;

        public SCharacterKillEnemy(IGameStateService gameStateService)
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

            component.Enemies
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    if (component.Enemies.Count == 0)
                    {
                        _gameStateService.Enter<StateWin>();
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
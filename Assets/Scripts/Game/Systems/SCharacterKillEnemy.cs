using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.States;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterKillEnemy : SystemComponent<CCharacter>
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IGameStateService _gameStateService;

        public SCharacterKillEnemy(IProgressService progressService, ISaveLoadService saveLoadService, IGameStateService gameStateService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
                .Subscribe(enemy =>
                {
                    _progressService.PlayerProgress.Money.Value += enemy.Value.Stats.Money;
                    _saveLoadService.SaveProgress();

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
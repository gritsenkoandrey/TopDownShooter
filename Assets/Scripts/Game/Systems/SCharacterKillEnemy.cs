using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.UI.Screens;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterKillEnemy : SystemComponent<CCharacter>
    {
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IUIFactory _uiFactory;

        public SCharacterKillEnemy(IProgressService progressService, ISaveLoadService saveLoadService, IUIFactory uiFactory)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _uiFactory = uiFactory;
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
                        _uiFactory.CreateScreen(ScreenType.Win);
                        _progressService.PlayerProgress.Level++;
                        _saveLoadService.SaveProgress();
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
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterKillEnemy : SystemComponent<CCharacter>
    {
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IUIFactory _uiFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _progressService = AllServices.Container.Single<IProgressService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _uiFactory = AllServices.Container.Single<IUIFactory>();
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
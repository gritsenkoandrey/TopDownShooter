using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeButton : SystemComponent<CUpgradeButton>
    {
        private ISaveLoadService _saveLoadService;
        private IProgressService _progressService;
        private IUIFactory _uiFactory;
        private IGameFactory _gameFactory;

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _progressService = AllServices.Container.Single<IProgressService>();
            _uiFactory = AllServices.Container.Single<IUIFactory>();
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

        protected override void OnEnableComponent(CUpgradeButton component)
        {
            base.OnEnableComponent(component);

            component.BuyButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.Level++;
                    
                    _progressService.PlayerProgress.Money.Value -= component.Cost;
                    _saveLoadService.SaveProgress();
                    
                    UpdateProgress();
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CUpgradeButton component)
        {
            base.OnDisableComponent(component);
        }
        
        private void UpdateProgress()
        {
            foreach (IProgressReader progress in _uiFactory.ProgressReaders)
            {
                progress.Read(_progressService.PlayerProgress);
            }

            foreach (IProgressReader progress in _gameFactory.ProgressReaders)
            {
                progress.Read(_progressService.PlayerProgress);
            }
        }
    }
}
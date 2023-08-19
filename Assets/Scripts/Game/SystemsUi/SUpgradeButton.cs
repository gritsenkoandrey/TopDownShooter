using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeButton : SystemComponent<CUpgradeButton>
    {
        private readonly ISaveLoadService _saveLoadService;
        private readonly IProgressService _progressService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;

        public SUpgradeButton(ISaveLoadService saveLoadService, IProgressService progressService, IUIFactory uiFactory, IGameFactory gameFactory)
        {
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _uiFactory = uiFactory;
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

        protected override void OnEnableComponent(CUpgradeButton component)
        {
            base.OnEnableComponent(component);

            component.BuyButton
                .OnClickAsObservable()
                .ThrottleFirst(Time())
                .Subscribe(_ =>
                {
                    component.Level++;
                    component.BuyButton.transform.PunchTransform();
                    
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
        
        private TimeSpan Time() => TimeSpan.FromSeconds(0.25f);
    }
}
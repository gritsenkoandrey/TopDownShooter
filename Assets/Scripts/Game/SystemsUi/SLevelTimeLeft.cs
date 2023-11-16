using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelTimeLeft : SystemComponent<CLevelTimeLeft>
    {
        private readonly IGameStateService _gameStateService;
        private readonly IGameFactory _gameFactory;
        
        public SLevelTimeLeft(IGameStateService gameStateService, IGameFactory gameFactory)
        {
            _gameStateService = gameStateService;
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

        protected override void OnEnableComponent(CLevelTimeLeft component)
        {
            base.OnEnableComponent(component);

            int time = _gameFactory.Level.LevelTime;

            Observable.EveryUpdate()
                .ThrottleFirst(Time())
                .Subscribe(_ => UpdateTime(component, ref time))
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CLevelTimeLeft component)
        {
            base.OnDisableComponent(component);
        }

        private TimeSpan Time() => TimeSpan.FromSeconds(1f);

        private void UpdateTime(CLevelTimeLeft component, ref int time)
        {
            if (time == 0)
            {
                _gameStateService.Enter<StateFail>();
                        
                return;
            }
                    
            time -= 1;

            component.TimeLeftText.text = FormatTime.SecondsToTime(time);
        }
    }
}
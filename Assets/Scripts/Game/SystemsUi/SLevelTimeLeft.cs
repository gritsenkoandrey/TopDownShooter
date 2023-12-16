using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Interfaces;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelTimeLeft : SystemComponent<CLevelTimeLeft>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IGameStateService _gameStateService;
        
        public SLevelTimeLeft(IGameFactory gameFactory, IGameStateService gameStateService)
        {
            _gameFactory = gameFactory;
            _gameStateService = gameStateService;
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

        private TimeSpan Time() => TimeSpan.FromSeconds(1f);

        private void UpdateTime(CLevelTimeLeft component, ref int time)
        {
            if (time == 0)
            {
                TimeLeft();
                
                return;
            }
                    
            time -= 1;

            component.TimeLeftText.text = FormatTime.SecondsToTime(time);
        }

        private void TimeLeft()
        {
            _gameStateService.Enter<StateFail>();
            
            _gameFactory.Character.StateMachine.StateMachine.Enter<CharacterStateNone>();
                
            foreach (IEnemy enemy in _gameFactory.Enemies)
            {
                enemy.StateMachine.StateMachine.Enter<ZombieStateNone>();
            }
        }
    }
}
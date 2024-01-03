using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.StateMachine.Zombie;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.States;
using CodeBase.Utils;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelTimeLeft : SystemComponent<CLevelTimeLeft>
    {
        private readonly IGameStateService _gameStateService;
        private readonly LevelModel _levelModel;

        public SLevelTimeLeft(IGameStateService gameStateService, LevelModel levelModel)
        {
            _gameStateService = gameStateService;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CLevelTimeLeft component)
        {
            base.OnEnableComponent(component);

            int time = _levelModel.Level.LevelTime;

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
            
            _levelModel.Character.StateMachine.StateMachine.Enter<CharacterStateNone>();
            _levelModel.Enemies.Foreach(enemy => enemy.StateMachine.StateMachine.Enter<ZombieStateNone>());
        }
    }
}
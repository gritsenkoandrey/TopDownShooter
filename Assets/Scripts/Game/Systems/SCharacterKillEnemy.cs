﻿using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.States;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterKillEnemy : SystemComponent<CCharacter>
    {
        private readonly IGameStateService _gameStateService;
        private readonly IGameFactory _gameFactory;

        public SCharacterKillEnemy(IGameStateService gameStateService, IGameFactory gameFactory)
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

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            _gameFactory.Enemies
                .ObserveRemove()
                .Subscribe(_ =>
                {
                    if (_gameFactory.Enemies.Count == 0)
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
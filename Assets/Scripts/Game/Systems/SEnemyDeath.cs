﻿using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SEnemyDeath : SystemComponent<CEnemy>
    {
        private readonly IGameFactory _gameFactory;

        public SEnemyDeath(IGameFactory gameFactory)
        {
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

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CEnemy component)
        {
            base.OnEnableComponent(component);
            
            component.Health.Health
                .SkipLatestValueOnSubscribe()
                .ObserveOnMainThread()
                .Subscribe(health =>
                {
                    if (health <= 0)
                    {
                        component.Agent.ResetPath();
                        component.Radar.Clear.Execute();
                        component.LifetimeDisposable.Clear();

                        _gameFactory.CurrentCharacter.Enemies.Remove(component);
                        
                        Transform prefab = _gameFactory.CreateDeathFx(component.transform.position.AddY(1f));
                        
                        DOVirtual.DelayedCall(2f, () => Object.Destroy(prefab.gameObject));
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CEnemy component)
        {
            base.OnDisableComponent(component);
        }
    }
}
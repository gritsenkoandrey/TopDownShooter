using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Pool;
using CodeBase.Utils;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieDeath : SystemComponent<CZombie>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IObjectPoolService _objectPoolService;

        public SZombieDeath(IGameFactory gameFactory, IObjectPoolService objectPoolService)
        {
            _gameFactory = gameFactory;
            _objectPoolService = objectPoolService;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CZombie component)
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
                        
                        GameObject prefab = _gameFactory.CreateDeathFx(component.transform.position.AddY(1f));
                        
                        DOVirtual.DelayedCall(2f, () => _objectPoolService.ReleaseObject(prefab));
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CZombie component)
        {
            base.OnDisableComponent(component);
        }
    }
}
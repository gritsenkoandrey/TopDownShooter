using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using CodeBase.UI;
using CodeBase.UI.Factories;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SUIUpdateHealth : SystemComponent<CUIHealth>
    {
        private IUIFactory _uiFactory;
        private IGameFactory _gameFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

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

        protected override void OnEnableComponent(CUIHealth component)
        {
            base.OnEnableComponent(component);

            InstantiateHealth(component);

            _gameFactory.CurrentCharacter.Health.Hit
                .Subscribe(_ =>
                {
                    if (component.Healths.Count > 0)
                    {
                        GameObject hp = component.Healths.Pop();
                        
                        Object.Destroy(hp);
                    }
                    else
                    {
                        _uiFactory.CreateScreen(ScreenType.Result);
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CUIHealth component)
        {
            base.OnDisableComponent(component);
        }

        private void InstantiateHealth(CUIHealth component)
        {
            for (int i = 0; i < 6; i++)
            {
                GameObject hp = Object.Instantiate(component.HP, component.Root);

                component.Healths.Push(hp);
            }
        }
    }
}
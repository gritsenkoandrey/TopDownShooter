using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SInput : SystemComponent<CInput>
    {
        private IGameFactory _gameFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _gameFactory = AllServices.Container.Single<IGameFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();

            foreach (CInput input in Entities)
            {
                input.UpdateInput.Execute();
            }
        }

        protected override void OnEnableComponent(CInput component)
        {
            base.OnEnableComponent(component);

            component.UpdateInput
                .Subscribe(_ => _gameFactory.CurrentCharacter.Value = component.Input.Value)
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CInput component)
        {
            base.OnDisableComponent(component);
            
            component.Input.Value = Vector2.zero;
        }
    }
}
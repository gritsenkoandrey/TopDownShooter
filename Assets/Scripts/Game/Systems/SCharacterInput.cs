using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterInput : SystemComponent<CInput>
    {
        private readonly IGameFactory _gameFactory;

        public SCharacterInput(IGameFactory gameFactory)
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

        protected override void OnUpdate()
        {
            base.OnUpdate();

            foreach (CInput input in Entities)
            {
                input.UpdateInput.Execute();
            }
        }

        protected override void OnEnableComponent(CInput component)
        {
            base.OnEnableComponent(component);

            component.UpdateInput
                .Subscribe(_ =>
                {
                    _gameFactory.CurrentCharacter.Move.Input = component.Input.Vector;
                    component.Input.OnUpdate();
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CInput component)
        {
            base.OnDisableComponent(component);
            
            _gameFactory.CurrentCharacter.Move.Input = Vector2.zero;
        }
    }
}
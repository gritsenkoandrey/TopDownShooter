using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Game.Systems
{
    public sealed class SVirtualCamera : SystemComponent<CVirtualCamera>
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
        }

        protected override void OnEnableComponent(CVirtualCamera component)
        {
            base.OnEnableComponent(component);

            component.SetTarget(_gameFactory.CurrentCharacter.Object.transform);
        }

        protected override void OnDisableComponent(CVirtualCamera component)
        {
            base.OnDisableComponent(component);
        }
    }
}
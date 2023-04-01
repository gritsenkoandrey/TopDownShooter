using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;

namespace CodeBase.Game.Systems
{
    public sealed class SVirtualCamera : SystemComponent<CVirtualCamera>
    {
        private readonly IGameFactory _gameFactory;

        public SVirtualCamera(IGameFactory gameFactory)
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

        protected override void OnEnableComponent(CVirtualCamera component)
        {
            base.OnEnableComponent(component);

            component.SetTarget(_gameFactory.CurrentCharacter.gameObject.transform);
        }

        protected override void OnDisableComponent(CVirtualCamera component)
        {
            base.OnDisableComponent(component);
        }
    }
}
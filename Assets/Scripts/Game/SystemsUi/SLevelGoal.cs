using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Services;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelGoal : SystemComponent<CLevelGoal>
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

        protected override void OnEnableComponent(CLevelGoal component)
        {
            base.OnEnableComponent(component);

            int max = _gameFactory.CurrentCharacter.Enemies.Count;
            
            component.TextLevelGoal.text = $"{max}/{max}";

            _gameFactory.CurrentCharacter.Enemies
                .ObserveCountChanged()
                .Subscribe(count =>
                {
                    if (count > 0)
                    {
                        component.TextLevelGoal.text = $"{count}/{max}";
                    }
                    else
                    {
                        component.Background.SetActive(false);
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CLevelGoal component)
        {
            base.OnDisableComponent(component);
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.Game;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelGoal : SystemComponent<CLevelGoal>
    {
        private readonly IGameFactory _gameFactory;

        public SLevelGoal(IGameFactory gameFactory)
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

        protected override void OnEnableComponent(CLevelGoal component)
        {
            base.OnEnableComponent(component);

            int max = _gameFactory.Enemies.Count;
            
            component.TextLevelGoal.text = max.ToString();

            _gameFactory.Enemies
                .ObserveCountChanged()
                .Subscribe(count =>
                {
                    if (count > 0)
                    {
                        component.TextLevelGoal.text = count.ToString();
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
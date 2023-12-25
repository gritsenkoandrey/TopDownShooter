using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelGoal : SystemComponent<CLevelGoal>
    {
        private readonly LevelModel _levelModel;

        public SLevelGoal(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CLevelGoal component)
        {
            base.OnEnableComponent(component);

            int max = _levelModel.Enemies.Count;
            
            component.TextLevelGoal.text = max.ToString();

            _levelModel.Enemies
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
    }
}
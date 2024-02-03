using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SLevelGoal : SystemComponent<CLevelGoal>
    {
        private LevelModel _levelModel;

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CLevelGoal component)
        {
            base.OnEnableComponent(component);
            
            SubscribeOnLevelGoal(component);
        }

        private void SubscribeOnLevelGoal(CLevelGoal component)
        {
            _levelModel.Enemies
                .ObserveCountChanged()
                .DoOnSubscribe(() => SetLevelGoalText(component, _levelModel.Enemies.Count))
                .Subscribe(count => UpdateLevelGoal(component, count))
                .AddTo(component.LifetimeDisposable);
        }

        private void SetLevelGoalText(CLevelGoal component, int count)
        {
            component.TextLevelGoal.text = count + SpriteAssetExtension.Target;
        }

        private void UpdateLevelGoal(CLevelGoal component, int count)
        {
            SetLevelGoalText(component, count);

            if (count <= 0)
            {
                component.Background.SetActive(false);
            }
        }
    }
}
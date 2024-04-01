using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SScreenVisualLevelGoal : SystemComponent<CLevelGoal>
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
                .DoOnSubscribe(() => SetStartValue(component))
                .Subscribe(count => UpdateLevelGoal(component, count))
                .AddTo(component.LifetimeDisposable);
        }

        private void SetStartValue(CLevelGoal component)
        {
            component.TextLevelGoal.text = _levelModel.Enemies.Count.ToString();
        }

        private void UpdateLevelGoal(CLevelGoal component, int count)
        {
            component.TextLevelGoal.text = count.ToString();

            if (count <= 0)
            {
                component.Background.SetActive(false);
            }
        }
    }
}
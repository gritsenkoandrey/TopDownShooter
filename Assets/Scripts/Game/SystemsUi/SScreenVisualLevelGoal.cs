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
            void SetStartValue() => component.TextLevelGoal.text = _levelModel.Enemies.Count.ToString();
            
            void UpdateLevelGoal(int count)
            {
                component.TextLevelGoal.text = count.ToString();

                if (count <= 0)
                {
                    component.Background.SetActive(false);
                }
            }

            _levelModel.Enemies
                .ObserveCountChanged()
                .DoOnSubscribe(SetStartValue)
                .Subscribe(UpdateLevelGoal)
                .AddTo(component.LifetimeDisposable);
        }
    }
}
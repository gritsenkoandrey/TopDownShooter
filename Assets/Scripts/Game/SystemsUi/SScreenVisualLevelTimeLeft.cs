using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SScreenVisualLevelTimeLeft : SystemComponent<CLevelTimeLeft>
    {
        private LevelModel _levelModel;

        [Inject]
        private void Construct(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CLevelTimeLeft component)
        {
            base.OnEnableComponent(component);

            SubscribeOnUpdateTimeLeft(component);
        }

        private void SubscribeOnUpdateTimeLeft(CLevelTimeLeft component)
        {
            void SetTime(int time) => component.TimeLeftText.text = time.SecondsToTime();

            _levelModel.Level
                .ObserveEveryValueChanged(level => level.Time)
                .Subscribe(SetTime)
                .AddTo(component.LifetimeDisposable);
        }
    }
}
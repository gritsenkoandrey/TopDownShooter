using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SPause : SystemComponent<CPause>
    {
        private PauseModel _pauseModel;

        [Inject]
        private void Construct(PauseModel pauseModel)
        {
            _pauseModel = pauseModel;
        }

        protected override void OnEnableComponent(CPause component)
        {
            base.OnEnableComponent(component);

            _pauseModel.OnPause
                .Subscribe(component.Pause)
                .AddTo(component.LifetimeDisposable);
        }
    }
}
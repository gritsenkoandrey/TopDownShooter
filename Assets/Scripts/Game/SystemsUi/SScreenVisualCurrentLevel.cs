using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SScreenVisualCurrentLevel : SystemComponent<CCurrentLevel>
    {
        private IProgressService _progressService;

        [Inject]
        private void Construct(IProgressService progressService)
        {
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CCurrentLevel component)
        {
            base.OnEnableComponent(component);

            _progressService.LevelData.Data
                .Subscribe(level => component.TextLevel.text = string.Format(FormatText.Level, level.ToString()))
                .AddTo(component.LifetimeDisposable);
        }
    }
}
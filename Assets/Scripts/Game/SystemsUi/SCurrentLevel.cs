using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCurrentLevel : SystemComponent<CCurrentLevel>
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

            component.TextLevel.text = string.Format(FormatText.Level, _progressService.LevelData.Data.Value.ToString());
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Progress;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCurrentLevel : SystemComponent<CCurrentLevel>
    {
        private readonly IProgressService _progressService;

        public SCurrentLevel(IProgressService progressService)
        {
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CCurrentLevel component)
        {
            base.OnEnableComponent(component);

            component.TextLevel.SetText("Level {0}", _progressService.LevelData.Data.Value);
        }
    }
}
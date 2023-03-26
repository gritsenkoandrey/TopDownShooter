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
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CCurrentLevel component)
        {
            base.OnEnableComponent(component);

            component.TextLevel.text = $"Level {_progressService.PlayerProgress.Level}";
        }

        protected override void OnDisableComponent(CCurrentLevel component)
        {
            base.OnDisableComponent(component);
        }
    }
}
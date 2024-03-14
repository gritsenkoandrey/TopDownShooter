using CodeBase.ECSCore;
using CodeBase.Infrastructure.Haptic;
using CodeBase.Infrastructure.Progress;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SHapticController : SystemBase
    {
        private IProgressService _progressService;
        private IHapticService _hapticService;

        [Inject]
        private void Construct(IProgressService progressService, IHapticService hapticService)
        {
            _progressService = progressService;
            _hapticService = hapticService;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _progressService.HapticData.Data
                .Subscribe(_hapticService.IsEnable)
                .AddTo(LifetimeDisposable);
        }
    }
}
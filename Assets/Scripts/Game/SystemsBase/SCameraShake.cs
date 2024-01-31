using CodeBase.ECSCore;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Models;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SCameraShake : SystemBase
    {
        private ICameraService _cameraService;
        private DamageCombatLog _damageCombatLog;

        [Inject]
        public void Construct(ICameraService cameraService, DamageCombatLog damageCombatLog)
        {
            _cameraService = cameraService;
            _damageCombatLog = damageCombatLog;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _damageCombatLog.CombatLog
                .Subscribe(_ => _cameraService.Shake())
                .AddTo(LifetimeDisposable);
        }
    }
}
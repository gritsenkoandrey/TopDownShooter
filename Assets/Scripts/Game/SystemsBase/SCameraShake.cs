using CodeBase.ECSCore;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Models;
using UniRx;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SCameraShake : SystemBase
    {
        private readonly ICameraService _cameraService;
        private readonly DamageCombatLog _damageCombatLog;

        public SCameraShake(ICameraService cameraService, DamageCombatLog damageCombatLog)
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
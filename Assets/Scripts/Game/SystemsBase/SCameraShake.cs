using CodeBase.ECSCore;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Models;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SCameraShake : SystemBase
    {
        private ICameraService _cameraService;
        private DamageCombatLog _damageCombatLog;

        [Inject]
        private void Construct(ICameraService cameraService, DamageCombatLog damageCombatLog)
        {
            _cameraService = cameraService;
            _damageCombatLog = damageCombatLog;
        }
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            _damageCombatLog.OnCombatLog += OnCombatLog;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            _damageCombatLog.OnCombatLog -= OnCombatLog;
        }

        private void OnCombatLog(CombatLog log) => _cameraService.Shake();
    }
}
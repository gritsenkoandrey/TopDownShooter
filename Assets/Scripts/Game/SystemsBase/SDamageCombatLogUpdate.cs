using CodeBase.ECSCore;
using CodeBase.Infrastructure.Models;
using VContainer;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SDamageCombatLogUpdate : SystemBase
    {
        private DamageCombatLog _damageCombatLog;

        [Inject]
        public void Construct(DamageCombatLog damageCombatLog)
        {
            _damageCombatLog = damageCombatLog;
        }
        
        protected override void OnLateUpdate()
        {
            base.OnLateUpdate();
            
            _damageCombatLog.Execute();
        }
    }
}
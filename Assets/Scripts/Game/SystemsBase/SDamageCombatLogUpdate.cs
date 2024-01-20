using CodeBase.ECSCore;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.SystemsBase
{
    public sealed class SDamageCombatLogUpdate : SystemBase
    {
        private readonly DamageCombatLog _damageCombatLog;

        public SDamageCombatLogUpdate(DamageCombatLog damageCombatLog)
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
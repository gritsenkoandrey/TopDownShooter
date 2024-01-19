using System.Collections.Generic;
using CodeBase.Game.Interfaces;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class DamageCombatLog
    {
        private readonly Queue<(IEnemy, int)> _damageCombatLog;

        public DamageCombatLog()
        {
            _damageCombatLog = new Queue<(IEnemy, int)>();
        }

        public bool HasDamage() => _damageCombatLog.Count > 0;
        public void Enqueue(IEnemy target, int damage) => _damageCombatLog.Enqueue((target, damage));
        public (IEnemy, int) Dequeue() => _damageCombatLog.Dequeue();
        public void CleanUp() => _damageCombatLog.Clear();
    }
}
using System.Collections.Generic;
using CodeBase.Game.Interfaces;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Models
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class DamageCombatLog
    {
        public readonly ReactiveCommand<CombatLog> CombatLog = new ();

        private readonly Queue<CombatLog> _damageCombatLog;
        private const float UpdateLogTime = 0.1f;
        private float _time;

        public DamageCombatLog()
        {
            _damageCombatLog = new Queue<CombatLog>();
        }

        public void AddLog(IEnemy target, int damage)
        {
            _damageCombatLog.Enqueue(new CombatLog(target, damage));
        }

        public void Execute()
        {
            if (_damageCombatLog.Count == 0)
            {
                return;
            }
            
            _time += Time.deltaTime;

            if (_time > UpdateLogTime)
            {
                _time = 0f;

                CombatLog.Execute(_damageCombatLog.Dequeue());
            }
        }


        public void CleanUp() => _damageCombatLog.Clear();
    }

    public readonly struct CombatLog
    {
        public readonly IEnemy Target;
        public readonly int Damage;

        public CombatLog(IEnemy target, int damage)
        {
            Target = target;
            Damage = damage;
        }
    }
}
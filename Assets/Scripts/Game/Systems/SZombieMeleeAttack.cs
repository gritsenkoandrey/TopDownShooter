using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieMeleeAttack : SystemComponent<CZombie>
    {
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.OnCheckDamage
                .Where(_ => CheckDamage(component))
                .Subscribe(_ => Damage(component))
                .AddTo(component.LifetimeDisposable);
        }

        private bool CheckDamage(CZombie component)
        {
            float distance = Vector3.Distance(component.Position, component.Target.Value.Move.Position);
            
            if (distance > component.Stats.MinDistanceToTarget || !component.Health.IsAlive)
            {
                return false;
            }

            return true;
        }

        private void Damage(CZombie component)
        {
            component.Target.Value.Health.CurrentHealth.Value -= component.Damage;
        }
    }
}
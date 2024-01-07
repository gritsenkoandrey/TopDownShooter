using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieMeleeAttack : SystemComponent<CZombie>
    {
        private readonly LevelModel _levelModel;

        public SZombieMeleeAttack(LevelModel levelModel)
        {
            _levelModel = levelModel;
        }
        
        protected override void OnEnableComponent(CZombie component)
        {
            base.OnEnableComponent(component);

            component.OnCheckDamage
                .Where(_ => IsCanDamage(component))
                .Subscribe(_ => Damage(component))
                .AddTo(component.LifetimeDisposable);
        }

        private bool IsCanDamage(CZombie component)
        {
            float distance = Vector3.Distance(component.Position, _levelModel.Character.Move.Position);
            
            if (distance > component.Stats.MinDistanceToTarget || !component.Health.IsAlive)
            {
                return false;
            }

            return true;
        }

        private void Damage(CZombie component)
        {
            _levelModel.Character.Health.CurrentHealth.Value -= component.Damage;
        }
    }
}
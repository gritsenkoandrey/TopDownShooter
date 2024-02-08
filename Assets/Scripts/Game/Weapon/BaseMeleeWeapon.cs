using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseMeleeWeapon : BaseWeapon
    {
        private protected IEffectFactory EffectFactory;

        private Tween _checkDistanceTween;

        protected BaseMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
        }

        public override void Attack(ITarget target = null)
        {
            base.Attack(target);
            
            Hit(target);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _checkDistanceTween?.Kill();
        }
        
        private void Hit(ITarget target)
        {
            NotReadyAttack();
            ReduceClip();
            UpdateFireInterval();
            CheckDamage(target);

            if (ClipIsEmpty())
            {
                UpdateRechargeTime();
            }
        }

        private void CheckDamage(ITarget target)
        {
            _checkDistanceTween?.Kill();
            _checkDistanceTween = DOVirtual.DelayedCall(WeaponCharacteristic.FireInterval / 4f, 
                () => CheckDistance(target));
        }

        private void CheckDistance(ITarget target)
        {
            for (int i = 0; i < Weapon.SpawnPoints.Length; i++)
            {
                float distance = (Weapon.SpawnPoints[i].position - target.Position).sqrMagnitude;

                if (distance < AttackDistance() && target.Health.IsAlive)
                {
                    int damage = GetDamage(target);
                    
                    target.Health.CurrentHealth.Value -= damage;
                    
                    EffectFactory.CreateEffect(EffectType.Hit, target.Position).Forget();
                }
            }
        }
    }
}
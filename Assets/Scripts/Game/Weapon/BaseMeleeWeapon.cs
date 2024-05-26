using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.StaticData.Data;
using Cysharp.Threading.Tasks;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseMeleeWeapon : BaseWeapon
    {
        private protected IEffectFactory EffectFactory;

        private ITarget _target;

        protected BaseMeleeWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic) 
            : base(weapon, weaponCharacteristic)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            Weapon.OnHit += Hit;
        }

        public override void Attack(ITarget target = null)
        {
            base.Attack(target);

            _target = target;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            Weapon.OnHit -= Hit;
        }

        private void Hit()
        {
            CheckDistance();
        }

        private void CheckDistance()
        {
            for (int i = 0; i < Weapon.SpawnPoints.Length; i++)
            {
                float distance = (Weapon.SpawnPoints[i].position - _target.Position).sqrMagnitude;

                if (distance < AttackDistance && _target.Health.IsAlive)
                {
                    int damage = CalculateCriticalDamage(GetDamage());
                    _target.Health.CurrentHealth.Value -= damage;
                    SendCombatLog(_target, damage);
                    EffectFactory.CreateEffect(EffectType.Hit, _target.Position).Forget();
                }
            }
        }
    }
}
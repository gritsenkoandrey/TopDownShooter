using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.Models;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SMeleeWeaponSendCombatLog : SystemComponent<CWeapon>
    {
        private DamageCombatLog _damageCombatLog;

        [Inject]
        private void Construct(DamageCombatLog damageCombatLog)
        {
            _damageCombatLog = damageCombatLog;
        }
        
        protected override void OnEnableComponent(CWeapon component)
        {
            base.OnEnableComponent(component);

            component.OnSendCombatLog
                .Where(_ => component.ProjectileType == ProjectileType.None)
                .Subscribe(tuple =>
                {
                    (ITarget target, int damage) = tuple;
                    
                    _damageCombatLog.AddLog(target, damage);
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}
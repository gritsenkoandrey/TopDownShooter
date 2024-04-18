using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.StateMachine;
using CodeBase.Infrastructure.Factories.Weapon;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class STurretSpawner : SystemComponent<CTurretSpawner>
    {
        private IGameFactory _gameFactory;
        private IStateMachineFactory _stateMachineFactory;
        private IWeaponFactory _weaponFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory, IStateMachineFactory stateMachineFactory, IWeaponFactory weaponFactory)
        {
            _gameFactory = gameFactory;
            _stateMachineFactory = stateMachineFactory;
            _weaponFactory = weaponFactory;
        }
        
        protected override void OnEnableComponent(CTurretSpawner component)
        {
            base.OnEnableComponent(component);

            CreateTurret(component).Forget();
        }

        private async UniTaskVoid CreateTurret(CTurretSpawner component)
        {
            CTurret turret = await _gameFactory.CreateTurret(component.TurretType, component.Position, component.transform.parent);

            turret.Stats = component.TurretStats;
            turret.Weapon.SetWeapon(_weaponFactory.CreateTurretWeapon(turret.Weapon, component.WeaponCharacteristic));
            turret.Health.SetMaxHealth(component.TurretStats.Health);
            turret.Health.CurrentHealth.SetValueAndForceNotify(component.TurretStats.Health);
            turret.Radar.SetRadius(component.WeaponCharacteristic.AttackDistance);
            turret.StateMachine.CreateStateMachine(_stateMachineFactory.CreateTurretStateMachine(turret));
        }
    }
}
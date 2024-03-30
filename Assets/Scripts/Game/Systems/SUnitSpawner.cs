using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.StateMachine;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SUnitSpawner : SystemComponent<CUnitSpawner>
    {
        private IGameFactory _gameFactory;
        private IWeaponFactory _weaponFactory;
        private IStateMachineFactory _stateMachineFactory;

        [Inject]
        private void Construct(IGameFactory gameFactory, IWeaponFactory weaponFactory, IStateMachineFactory stateMachineFactory)
        {
            _gameFactory = gameFactory;
            _weaponFactory = weaponFactory;
            _stateMachineFactory = stateMachineFactory;
        }

        protected override void OnEnableComponent(CUnitSpawner component)
        {
            base.OnEnableComponent(component);
            
            CreateUnit(component).Forget();
        }
        
        private async UniTaskVoid CreateUnit(CUnitSpawner component)
        {
            CUnit unit = await _gameFactory.CreateUnit(component.Position, component.transform.parent);
            CWeapon weapon = await _weaponFactory
                .CreateUnitWeapon(component.WeaponType, component.WeaponCharacteristic, unit.WeaponMediator.Container);
            
            unit.WeaponMediator.SetWeapon(weapon);
            unit.UnitStats = component.UnitStats;
            unit.Health.SetMaxHealth(unit.UnitStats.Health);
            unit.Health.CurrentHealth.SetValueAndForceNotify(unit.UnitStats.Health);
            unit.Animator.Animator.runtimeAnimatorController = weapon.RuntimeAnimatorController;
            unit.StateMachine.CreateStateMachine(_stateMachineFactory.CreateUnitStateMachine(unit));
            
            SetEquipment(unit);
        }
        
        private void SetEquipment(CUnit unit)
        {
            int index = unit.BodyMediator.Skins.GetRandomIndex();
            
            for (int i = 0; i < unit.BodyMediator.Skins.Length; i++)
            for (int j = 0; j < unit.BodyMediator.Skins[i].Data.Visual.Length; j++)
            {
                unit.BodyMediator.Skins[i].Data.Visual[j].SetActive(i == index);
            }
        }
    }
}
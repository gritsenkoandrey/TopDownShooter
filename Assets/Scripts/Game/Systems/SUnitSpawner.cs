using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.StateMachine.Unit;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SUnitSpawner : SystemComponent<CUnitSpawner>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IWeaponFactory _weaponFactory;
        private readonly LevelModel _levelModel;

        public SUnitSpawner(IGameFactory gameFactory, IWeaponFactory weaponFactory, LevelModel levelModel)
        {
            _gameFactory = gameFactory;
            _weaponFactory = weaponFactory;
            _levelModel = levelModel;
        }

        protected override void OnEnableComponent(CUnitSpawner component)
        {
            base.OnEnableComponent(component);
            
            CreateUnit(component).Forget();
        }
        
        private async UniTaskVoid CreateUnit(CUnitSpawner component)
        {
            CUnit unit = await _gameFactory.CreateUnit(component.Position, component.transform.parent);
            
            CWeapon weapon = await _weaponFactory.CreateUnitWeapon(component.WeaponType, unit.WeaponMediator.Container);
            
            unit.WeaponMediator.SetWeapon(weapon);
            unit.UnitStats = component.UnitStats;
            unit.Health.SetMaxHealth(unit.UnitStats.Health);
            unit.Health.CurrentHealth.SetValueAndForceNotify(unit.UnitStats.Health);
            unit.Animator.Animator.runtimeAnimatorController = weapon.RuntimeAnimatorController;
            unit.Radar.SetRadius(weapon.Weapon.DetectionDistance());
            
            CreateStateMachine(unit);
            SetEquipment(unit);
        }

        private void CreateStateMachine(CUnit unit)
        {
            unit.StateMachine.SetStateMachine(new UnitStateMachine(unit, _levelModel));

            unit.StateMachine.UpdateStateMachine
                .Subscribe(_ => unit.StateMachine.StateMachine.Tick())
                .AddTo(unit.LifetimeDisposable);
        }
        
        private void SetEquipment(CUnit unit)
        {
            int index = Random.Range(0, unit.BodyMediator.Bodies.Length);
            
            for (int i = 0; i < unit.BodyMediator.Bodies.Length; i++)
            {
                unit.BodyMediator.Bodies[i].SetActive(index == i);
            }

            for (int i = 0; i < unit.BodyMediator.Heads.Length; i++)
            {
                unit.BodyMediator.Heads[i].SetActive(index == i);
            }
        }
    }
}
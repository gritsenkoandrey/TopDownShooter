using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.StateMachine;
using CodeBase.Infrastructure.Factories.Weapon;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterSpawner : SystemComponent<CCharacterSpawner>
    {
        private IGameFactory _gameFactory;
        private IProgressService _progressService;
        private IWeaponFactory _weaponFactory;
        private IStateMachineFactory _stateMachineFactory;
        private ICameraService _cameraService;
        private InventoryModel _inventoryModel;

        [Inject]
        private void Construct(IGameFactory gameFactory, IProgressService progressService, IWeaponFactory weaponFactory, 
            IStateMachineFactory stateMachineFactory, ICameraService cameraService, InventoryModel inventoryModel)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _weaponFactory = weaponFactory;
            _stateMachineFactory = stateMachineFactory;
            _inventoryModel = inventoryModel;
            _cameraService = cameraService;
        }
        
        protected override void OnEnableComponent(CCharacterSpawner component)
        {
            base.OnEnableComponent(component);
            
            CreateCharacter(component).Forget();
        }

        private async UniTaskVoid CreateCharacter(CCharacterSpawner component)
        {
            CCharacter character = await _gameFactory.CreateCharacter(_inventoryModel.SelectedSkin.Value, component.Position, component.transform.parent);
            CWeapon weapon = await _weaponFactory.CreateCharacterWeapon(_inventoryModel.SelectedWeapon.Value, character.WeaponMediator.Container);

            character.WeaponMediator.SetWeapon(weapon);
            character.Animator.Animator.runtimeAnimatorController = weapon.RuntimeAnimatorController;
            character.StateMachine.CreateStateMachine(_stateMachineFactory.CreateCharacterStateMachine(character));
            
            character.CharacterController.SetSpeed(character.CharacterController.BaseSpeed + 
                                                   _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Speed]);
            character.Health.SetMaxHealth(character.Health.BaseHealth * 
                                          _progressService.StatsData.Data.Value.Data[UpgradeButtonType.Health]);
            character.Health.CurrentHealth.SetValueAndForceNotify(character.Health.MaxHealth);

            SetEquipment(character);
            SetCameraTarget(character);
            SetRadarRadius(character);
        }

        private void SetEquipment(CCharacter character)
        {
            for (int i = 0; i < character.BodyMediator.Skins.Length; i++)
            for (int j = 0; j < character.BodyMediator.Skins[i].Data.Visual.Length; j++)
            {
                character.BodyMediator.Skins[i].Data.Visual[j]
                    .SetActive(character.BodyMediator.Skins[i].Type == _inventoryModel.SelectedSkin.Value);
            }
        }

        private void SetCameraTarget(CCharacter character)
        {
            _cameraService.SetTarget(character.transform);
        }

        private void SetRadarRadius(CCharacter character)
        {
            float distance = Mathf.Sqrt(character.WeaponMediator.CurrentWeapon.Weapon.AttackDistance());
            character.Radar.SetRadius(distance);
        }
    }
}
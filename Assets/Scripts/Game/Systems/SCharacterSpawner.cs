using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.StateMachine.Character;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterSpawner : SystemComponent<CCharacterSpawner>
    {
        private readonly IGameFactory _gameFactory;
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;
        private readonly IProgressService _progressService;
        private readonly IWeaponFactory _weaponFactory;
        private readonly InventoryModel _inventoryModel;
        private readonly LevelModel _levelModel;

        public SCharacterSpawner(IGameFactory gameFactory, ICameraService cameraService, IJoystickService joystickService, 
            IProgressService progressService, IWeaponFactory weaponFactory, InventoryModel inventoryModel, LevelModel levelModel)
        {
            _gameFactory = gameFactory;
            _cameraService = cameraService;
            _joystickService = joystickService;
            _progressService = progressService;
            _weaponFactory = weaponFactory;
            _inventoryModel = inventoryModel;
            _levelModel = levelModel;
        }
        
        protected override void OnEnableComponent(CCharacterSpawner component)
        {
            base.OnEnableComponent(component);
            
            CreateCharacter(component).Forget();
        }

        private async UniTaskVoid CreateCharacter(CCharacterSpawner component)
        {
            CCharacter character = await _gameFactory.CreateCharacter(component.Position, component.transform.parent);

            SubscribeOnUpgradeHealth(character);
            SubscribeOnUpgradeSpeed(character);
            CreateStateMachine(character);
            CreateWeapon(character).Forget();
            SetEquipment(character);
        }

        private void SubscribeOnUpgradeSpeed(CCharacter character)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(stats => stats.Data[UpgradeButtonType.Speed])
                .Subscribe(speed => character.Move.SetSpeed(character.Move.BaseSpeed + speed))
                .AddTo(character.Entity.LifetimeDisposable);
        }

        private void SubscribeOnUpgradeHealth(CCharacter character)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(stats => stats.Data[UpgradeButtonType.Health])
                .Subscribe(health =>
                {
                    character.Health.SetMaxHealth(character.Health.BaseHealth * health);
                    character.Health.CurrentHealth.SetValueAndForceNotify(character.Health.MaxHealth);
                })
                .AddTo(character.Entity.LifetimeDisposable);
        }

        private void CreateStateMachine(CCharacter character)
        {
            character.StateMachine.SetStateMachine(new CharacterStateMachine(character, _cameraService, _joystickService, _levelModel));

            character.StateMachine.UpdateStateMachine
                .Subscribe(_ => character.StateMachine.StateMachine.Tick())
                .AddTo(character.Entity.LifetimeDisposable);
        }
        
        private async UniTaskVoid CreateWeapon(CCharacter character)
        {
            CWeapon weapon = await _weaponFactory.CreateCharacterWeapon(_inventoryModel.SelectedWeapon.Value, character.WeaponMediator.Container);
            
            character.WeaponMediator.SetWeapon(weapon);
            character.Animator.Animator.runtimeAnimatorController = weapon.RuntimeAnimatorController;
        }
        
        private void SetEquipment(CCharacter character)
        {
            for (int i = 0; i < character.BodyMediator.Bodies.Length; i++)
            {
                character.BodyMediator.Bodies[i].SetActive(_inventoryModel.EquipmentIndex.Value == i);
            }

            for (int i = 0; i < character.BodyMediator.Heads.Length; i++)
            {
                character.BodyMediator.Heads[i].SetActive(_inventoryModel.EquipmentIndex.Value == i);
            }
        }
    }
}
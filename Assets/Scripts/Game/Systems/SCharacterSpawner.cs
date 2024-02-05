using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.StateMachine;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterSpawner : SystemComponent<CCharacterSpawner>
    {
        private IGameFactory _gameFactory;
        private IProgressService _progressService;
        private IWeaponFactory _weaponFactory;
        private IStateMachineFactory _stateMachineFactory;
        private InventoryModel _inventoryModel;

        [Inject]
        private void Construct(IGameFactory gameFactory, IProgressService progressService, IWeaponFactory weaponFactory, 
            IStateMachineFactory stateMachineFactory, InventoryModel inventoryModel)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
            _weaponFactory = weaponFactory;
            _stateMachineFactory = stateMachineFactory;
            _inventoryModel = inventoryModel;
        }
        
        protected override void OnEnableComponent(CCharacterSpawner component)
        {
            base.OnEnableComponent(component);
            
            CreateCharacter(component).Forget();
        }

        private async UniTaskVoid CreateCharacter(CCharacterSpawner component)
        {
            CCharacter character = await _gameFactory.CreateCharacter(component.Position, component.transform.parent);
            CWeapon weapon = await _weaponFactory.CreateCharacterWeapon(_inventoryModel.SelectedWeapon.Value, character.WeaponMediator.Container);

            character.WeaponMediator.SetWeapon(weapon);
            character.Animator.Animator.runtimeAnimatorController = weapon.RuntimeAnimatorController;
            character.StateMachine.SetStateMachine(_stateMachineFactory.CreateCharacterStateMachine(character));

            SubscribeOnUpgradeHealth(character);
            SubscribeOnUpgradeSpeed(character);
            SubscribeOnUpdateStateMachine(character);
            SetEquipment(character);
        }

        private void SubscribeOnUpgradeSpeed(CCharacter character)
        {
            _progressService.StatsData.Data.Value
                .ObserveEveryValueChanged(stats => stats.Data[UpgradeButtonType.Speed])
                .Subscribe(speed => character.Move.SetSpeed(character.Move.BaseSpeed + speed))
                .AddTo(character.LifetimeDisposable);
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
                .AddTo(character.LifetimeDisposable);
        }

        private void SubscribeOnUpdateStateMachine(CCharacter character)
        {
            character.StateMachine.UpdateStateMachine
                .Subscribe(_ => character.StateMachine.StateMachine.Tick())
                .AddTo(character.LifetimeDisposable);
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
using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsBase;
using CodeBase.Game.SystemsUi;
using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using JetBrains.Annotations;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemEntryPoint : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
        private SystemBase[] _systems = Array.Empty<SystemBase>();

        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;
        private readonly ITextureArrayFactory _textureArrayFactory;
        private readonly IGuiService _guiService;
        private readonly IWeaponFactory _weaponFactory;
        private readonly IEffectFactory _effectFactory;

        private readonly InventoryModel _inventoryModel;
        private readonly LevelModel _levelModel;
        private readonly DamageCombatLog _damageCombatLog;

        public SystemEntryPoint(
            IUIFactory uiFactory, 
            IGameFactory gameFactory, 
            IProgressService progressService, 
            IObjectPoolService objectPoolService, 
            ICameraService cameraService, 
            IJoystickService joystickService,
            ITextureArrayFactory textureArrayFactory, 
            IGuiService guiService, 
            IWeaponFactory weaponFactory,
            IEffectFactory effectFactory,
            InventoryModel inventoryModel,
            LevelModel levelModel,
            DamageCombatLog damageCombatLog)
        {
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _objectPoolService = objectPoolService;
            _cameraService = cameraService;
            _joystickService = joystickService;
            _textureArrayFactory = textureArrayFactory;
            _guiService = guiService;
            _weaponFactory = weaponFactory;
            _effectFactory = effectFactory;
            _inventoryModel = inventoryModel;
            _levelModel = levelModel;
            _damageCombatLog = damageCombatLog;
        }

        void IInitializable.Initialize() => CreateSystems();
        void IStartable.Start() => EnableSystems();
        void IDisposable.Dispose() => Clear();
        void ITickable.Tick() => UpdateSystems();
        void IFixedTickable.FixedTick() => FixedUpdateSystems();
        void ILateTickable.LateTick() => LateUpdateSystems();

        private void CreateSystems()
        {
            _systems = new SystemBase[]
            {
                new SGroundBuildNavMesh(),
                new SRadarDraw(),
                new SSelectMesh(),
                new SUpgradeShop(_uiFactory),
                new SUpgradeButton(_progressService),
                new SLevelGoal(_levelModel),
                new SMoneyUpdate(_progressService),
                new SBulletLifeTime(_objectPoolService),
                new SCurrentLevel(_progressService),
                new SGroundMesh(_textureArrayFactory),
                new SDamageCombatLogViewUpdate(_cameraService),
                new SEnemyHealthProvider(_uiFactory, _levelModel),
                new SEnemyHealthUpdate(_cameraService),
                new SCharacterHealth(_levelModel),
                new SBulletProvider(),
                new SBulletCollision(_effectFactory, _levelModel, _damageCombatLog),
                new SLevelTimeLeft(_levelModel),
                new SStateMachineUpdate(),
                new SAnimator(),
                new SCharacterPreviewRotation(),
                new SCharacterPreviewMediator(_inventoryModel, _guiService),
                new SCharacterAmmunitionView(_inventoryModel),
                new SCharacterSpawner(_gameFactory, _cameraService, _joystickService, _progressService, _weaponFactory, _inventoryModel, _levelModel),
                new SPointerArrowProvider(_uiFactory, _levelModel),
                new SPointerArrowUpdate(_cameraService, _guiService),
                new SDamageCombatLogViewProvider(_uiFactory, _cameraService, _guiService, _damageCombatLog),
                new SDamageCombatLogUpdate(_damageCombatLog),
                new SJoystickUpdate(_joystickService),
                new SObjectPoolLog(_objectPoolService),
                new SUnitSpawner(_gameFactory, _weaponFactory, _levelModel),
                new SUnitDeath(_progressService, _effectFactory, _levelModel),
            };
        }

        private void EnableSystems()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].EnableSystem();
            }
        }

        private void DisableSystems()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].DisableSystem();
            }
        }

        private void UpdateSystems()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].Update();
            }
        }
        
        private void FixedUpdateSystems()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].FixedUpdate();
            }
        }
        
        private void LateUpdateSystems()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].LateUpdate();
            }
        }

        private void Clear()
        {
            DisableSystems();
            
            _systems = Array.Empty<SystemBase>();
        }
    }
}
using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.TextureArray;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.States;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class BootstrapEntryPoint : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
        private SystemBase[] _systems = Array.Empty<SystemBase>();

        private readonly IGameStateService _gameStateService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly ICameraService _cameraService;
        private readonly IJoystickService _joystickService;
        private readonly ITextureArrayFactory _textureArrayFactory;
        private readonly IGuiService _guiService;
        private readonly IObjectResolver _objectResolver;

        public BootstrapEntryPoint(IGameStateService gameStateService, IUIFactory uiFactory, 
            IGameFactory gameFactory, IProgressService progressService, ISaveLoadService saveLoadService, 
            IObjectPoolService objectPoolService, ICameraService cameraService, IJoystickService joystickService,
            ITextureArrayFactory textureArrayFactory, IGuiService guiService, IObjectResolver objectResolver)
        {
            _gameStateService = gameStateService;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _objectPoolService = objectPoolService;
            _cameraService = cameraService;
            _joystickService = joystickService;
            _textureArrayFactory = textureArrayFactory;
            _guiService = guiService;
            _objectResolver = objectResolver;
        }

        void IInitializable.Initialize() => CreateSystems();

        void IStartable.Start()
        {
            EnableSystems();
            
            _gameStateService.Enter<StateBootstrap>();
        }

        void IDisposable.Dispose() => Clear();
        void ITickable.Tick() => UpdateSystems();
        void IFixedTickable.FixedTick() => FixedUpdateSystems();
        void ILateTickable.LateTick() => LateUpdateSystems();

        private void CreateSystems()
        {
            _systems = new SystemBase[]
            {
                new SGroundBuildNavMesh(),
                new SCharacterStateMachine(_cameraService, _joystickService, _gameFactory),
                new SCharacterAnimator(),
                new SCharacterWeapon(_gameFactory),
                new SZombieStateMachine(),
                new SZombieAnimator(),
                new SZombieAggro(),
                new SZombieMeleeAttack(),
                new SZombieDeath(_gameFactory, _progressService, _saveLoadService),
                new SRadarDraw(),
                new SZombieSelectSkin(),
                new SUpgradeShop(_uiFactory),
                new SUpgradeButton(_saveLoadService, _progressService, _uiFactory, _gameFactory),
                new SLevelGoal(_gameFactory),
                new SMoneyUpdate(_progressService),
                new SBulletLifeTime(_objectPoolService),
                new SCurrentLevel(_progressService),
                new SGroundMesh(_textureArrayFactory),
                new SDamageView(_gameFactory, _cameraService),
                new SEnemyHealthProvider(_gameFactory, _uiFactory, _cameraService),
                new SEnemyHealth(),
                new SCharacterHealth(_gameFactory),
                new SBulletProvider(_gameFactory),
                new SLevelTimeLeft(_gameStateService, _gameFactory),
                new SLevelGameState(_gameStateService, _gameFactory),
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
            
            _joystickService.Execute();
            _objectPoolService.Log();
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
            
            _objectPoolService.CleanUp();
        }
    }
}
using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsUi;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Pool;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.States;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class BootstrapEntryPoint : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
        private SystemBase[] _systems;

        private readonly IGameStateService _gameStateService;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IObjectPoolService _objectPoolService;
        private readonly ICameraService _cameraService;

        public BootstrapEntryPoint(IGameStateService gameStateService, IUIFactory uiFactory, 
            IGameFactory gameFactory, IProgressService progressService, ISaveLoadService saveLoadService, 
            IObjectPoolService objectPoolService, ICameraService cameraService)
        {
            _gameStateService = gameStateService;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _objectPoolService = objectPoolService;
            _cameraService = cameraService;
        }

        void IInitializable.Initialize() => CreateSystems();

        void IStartable.Start()
        {
            EnableSystems();
            
            _gameStateService.Register();
            _gameStateService.Enter<StateBootstrap>();
        }

        void IDisposable.Dispose()
        {
            DisableSystems();
            Clear();
        }

        void ITickable.Tick() => UpdateSystems();
        void IFixedTickable.FixedTick() => FixedUpdateSystems();
        void ILateTickable.LateTick() => LateUpdateSystems();

        private void CreateSystems()
        {
            _systems = new SystemBase[]
            {
                new SGroundBuildNavMesh(),
                new SCharacterStateMachine(),
                new SCharacterAnimator(),
                new SCharacterWeapon(_gameFactory),
                new SCharacterDeath(_uiFactory),
                new SCharacterKillEnemy(_progressService, _saveLoadService, _uiFactory),
                new SCharacterInput(_gameFactory),
                new SZombieSpawner(_gameFactory),
                new SZombieStateMachine(),
                new SZombieAnimator(),
                new SZombieCollision(_gameFactory, _objectPoolService),
                new SZombieMeleeAttack(),
                new SZombieDeath(_gameFactory, _objectPoolService),
                new SHealthViewUpdate(_cameraService),
                new SRadarDraw(),
                new SVirtualCamera(_gameFactory),
                new SSelectMesh(),
                new SUpgradeShop(_uiFactory),
                new SUpgradeButton(_saveLoadService, _progressService, _uiFactory, _gameFactory),
                new SLevelGoal(_gameFactory),
                new SMoneyUpdate(_progressService),
                new SBulletLifeTime(_objectPoolService),
                new SCurrentLevel(_progressService),
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

        private void Clear() => _systems = Array.Empty<SystemBase>();
    }
}
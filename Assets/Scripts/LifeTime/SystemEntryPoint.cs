using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsUi;
using CodeBase.Infrastructure.Factories.Game;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Infrastructure.Services;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class SystemEntryPoint : IInitializable, IDisposable, ITickable
    {
        private SystemBase[] _systems;

        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        
        public SystemEntryPoint()
        {
            _uiFactory = AllServices.Container.Single<IUIFactory>();
            _gameFactory = AllServices.Container.Single<IGameFactory>();
            _progressService = AllServices.Container.Single<IProgressService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void Initialize()
        {
            CreateSystems();
            EnableSystems();
        }

        public void Dispose()
        {
            DisableSystems();
            Clear();
        }

        public void Tick()
        {
            UpdateSystems();
        }

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
                new SSpawnerZombie(_gameFactory),
                new SEnemyStateMachine(_gameFactory),
                new SEnemyAnimator(),
                new SEnemyCollision(),
                new SEnemyMeleeAttack(_gameFactory),
                new SEnemyDeath(_gameFactory),
                new SHealthViewUpdate(),
                new SRadarDraw(),
                new SVirtualCamera(_gameFactory),
                new SSelectMesh(),
                new SUpgradeShop(),
                new SUpgradeButton(),
                new SLevelGoal(),
                new SMoneyUpdate(),
                new SBulletLifeTime(),
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
                _systems[i].Tick();
            }
        }

        private void Clear()
        {
            _systems = Array.Empty<SystemBase>();
        }
    }
}
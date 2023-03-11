using System;
using System.Collections.Generic;
using AndreyGritsenko.Game.Components;
using AndreyGritsenko.Game.Systems;
using AndreyGritsenko.Infrastructure.Input;
using VContainer.Unity;

namespace AndreyGritsenko.LifeTime
{
    public sealed class InitSystems : IInitializable, IDisposable
    {
        private List<ECSCore.System> _systems;

        private readonly CCharacter _character;

        private readonly IInputService _inputService;

        public InitSystems(Joystick joystick, CCharacter character)
        {
            _character = character;

            _inputService = new InputService(joystick);
        }

        public void Initialize()
        {
            CreateSystems();
            EnableSystem();
        }

        public void Dispose()
        {
            DisableSystem();
        }

        private void CreateSystems()
        {
            _systems = new List<ECSCore.System>
            {
                new SCharacterController(_inputService),
                new SGroundBuildNavMesh(),
                new SSpawnerPrefab(),
                new SEnemyMovement(_character),
                new SRadarDraw(),
            };
        }

        private void EnableSystem()
        {
            foreach (ECSCore.System system in _systems)
            {
                system.EnableSystem();
            }
        }

        private void DisableSystem()
        {
            foreach (ECSCore.System system in _systems)
            {
                system.DisableSystem();
            }
        }
    }
}
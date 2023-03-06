using System;
using System.Collections.Generic;
using AndreyGritsenko.Game.Components;
using AndreyGritsenko.Game.Systems;
using SimpleInputNamespace;
using VContainer.Unity;

namespace AndreyGritsenko.Infrastructure
{
    public sealed class InitSystems : IInitializable, IDisposable
    {
        private List<ECSCore.System> _systems;

        private readonly Joystick _joystick;
        private readonly CCharacter _character;

        public InitSystems(Joystick joystick, CCharacter character)
        {
            _joystick = joystick;
            _character = character;
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
                new SCharacterController(_joystick),
                new SGroundBuildNavMesh(),
                new SSpawnerPrefab(),
                new SEnemyMovement(_character),
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
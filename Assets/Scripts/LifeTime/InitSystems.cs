using System;
using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Systems;
using CodeBase.Infrastructure.Input;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class InitSystems : IInitializable, IDisposable, ITickable
    {
        private List<SystemBase> _systems;

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
            _systems = new List<SystemBase>
            {
                new SGroundBuildNavMesh(),
                new SCharacterStateMachine(_inputService),
                new SCharacterAnimationController(),
                new SSpawnerPrefab(),
                new SEnemyInitialize(_character),
                new SEnemyStateMachine(_character),
                new SEnemyAnimationController(),
                new SRadarDraw(),
                new SVirtualCamera(_character),
                new SSelectMesh(),
                new SAttack(),
            };
        }

        private void EnableSystem()
        {
            foreach (SystemBase system in _systems)
            {
                system.EnableSystem();
            }
        }

        private void DisableSystem()
        {
            foreach (SystemBase system in _systems)
            {
                system.DisableSystem();
            }
        }

        public void Tick()
        {
            foreach (SystemBase system in _systems)
            {
                system.Tick();
            }
        }
    }
}
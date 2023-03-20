using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsUi;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class SystemEntryPoint : IInitializable, IDisposable, ITickable
    {
        private SystemBase[] _systems;

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
                new SCharacterWeapon(),
                new SCharacterDeath(),
                new SCharacterKillEnemy(),
                new SCharacterInput(),
                new SSpawnerZombie(),
                new SEnemyStateMachine(),
                new SEnemyAnimator(),
                new SEnemyCollision(),
                new SEnemyMeleeAttack(),
                new SEnemyDeath(),
                new SHealthViewUpdate(),
                new SRadarDraw(),
                new SVirtualCamera(),
                new SSelectMesh(),
                new SUpgradeShop(),
                new SUpgradeButton(),
                new SLevelGoal(),
                new SMoneyUpdate(),
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
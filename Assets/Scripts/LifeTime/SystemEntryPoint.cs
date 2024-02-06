using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsBase;
using CodeBase.Game.SystemsUi;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemEntryPoint : IInitializable, IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
    {
        private SystemBase[] _systems = Array.Empty<SystemBase>();

        private readonly IObjectResolver _objectResolver;

        public SystemEntryPoint(IObjectResolver objectResolver) => _objectResolver = objectResolver;

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
                new SUpgradeShop(),
                new SUpgradeButton(),
                new SLevelGoal(),
                new SMoneyUpdate(),
                new SBulletLifeTime(),
                new SCurrentLevel(),
                new SGroundMesh(),
                new SDamageCombatLogViewUpdate(),
                new SEnemyHealthProvider(),
                new SEnemyHealthUpdate(),
                new SCharacterHealth(),
                new SBulletMove(),
                new SBulletCollision(),
                new SLevelTimeLeft(),
                new SStateMachineUpdate(),
                new SAnimator(),
                new SCharacterPreviewRotation(),
                new SCharacterPreviewMediator(),
                new SCharacterAmmunitionView(),
                new SCharacterSpawner(),
                new SPointerArrowProvider(),
                new SPointerArrowUpdate(),
                new SDamageCombatLogViewProvider(),
                new SDamageCombatLogUpdate(),
                new SJoystickUpdate(),
                new SObjectPoolLog(),
                new SUnitSpawner(),
                new SBloodEffect(),
                new SCameraShake(),
                new SPrintResultText(),
                new SWeaponReloadingView(),
            };
            
            _systems.Foreach(_objectResolver.Inject);
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
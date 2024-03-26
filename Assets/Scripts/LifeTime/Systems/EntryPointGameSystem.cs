using System;
using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsBase;
using CodeBase.Game.SystemsUi;
using CodeBase.Utils;
using JetBrains.Annotations;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public class EntryPointGameSystem : IEntryPointSystem
    {
        private SystemBase[] _systems = Array.Empty<SystemBase>();

        private readonly IObjectResolver _objectResolver;

        public EntryPointGameSystem(IObjectResolver objectResolver) => _objectResolver = objectResolver;

        void IInitializable.Initialize()
        {
            _systems = new SystemBase[]
            {
                new SGroundBuildNavMesh(),
                new SRadarDraw(),
                new SSelectMesh(),
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
                new SCharacterAmmunitionView(),
                new SCharacterSpawner(),
                new SCharacterDeath(),
                new SPointerArrowProvider(),
                new SPointerArrowUpdate(),
                new SDamageCombatLogViewProvider(),
                new SDamageCombatLogUpdate(),
                new SJoystickUpdate(),
                new SObjectPoolLog(),
                new SUnitSpawner(),
                new SUnitDeath(),
                new SBloodEffect(),
                new SCameraShake(),
                new SPrintResultText(),
                new SWeaponReloadingView(),
                new SSettingsButton(),
                new SPause(),
                new SWinReward(),
                new SHapticController(),
                new SHapticButton(),
                new SToggleController(),
                new SSettingsMediator(),
                new SMoneyLootProvider(),
                new SMoneyLootUpdate(),
                new SDeathEffect(),
                new SDailyTaskUpdate(),
            };
            
            _systems.Foreach(_objectResolver.Inject);
        }
        
        void IStartable.Start() => _systems.Foreach(Enable);

        void ITickable.Tick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].Update();
            }
        }
        void IFixedTickable.FixedTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].FixedUpdate();
            }
        }
        void ILateTickable.LateTick()
        {
            for (int i = 0; i < _systems.Length; i++)
            {
                _systems[i].LateUpdate();
            }
        }
        
        void IDisposable.Dispose()
        {
            _systems.Foreach(Disable);
            _systems = Array.Empty<SystemBase>();
        }

        private void Enable(SystemBase system) => system.EnableSystem();
        private void Disable(SystemBase system) => system.DisableSystem();
    }
}
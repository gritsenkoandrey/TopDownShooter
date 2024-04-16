using CodeBase.ECSCore;
using CodeBase.Game.Systems;
using CodeBase.Game.SystemsBase;
using CodeBase.Game.SystemsUi;
using JetBrains.Annotations;

namespace CodeBase.Infrastructure.Factories.Systems
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class SystemFactory : ISystemFactory
    {
        SystemBase[] ISystemFactory.CreateGameSystems()
        {
            SystemBase[] systems = 
            {
                new SGroundBuildNavMesh(),
                new SRadarDraw(),
                new SSelectMesh(),
                new SScreenVisualLevelGoal(),
                new SMoneyUpdate(),
                new SBulletLifeTime(),
                new SScreenVisualCurrentLevel(),
                new SGroundMesh(),
                new SDamageCombatLogViewUpdate(),
                new SEnemyHealthProvider(),
                new SEnemyHealthUpdate(),
                new SScreenVisualCharacterHealth(),
                new SBulletMove(),
                new SBulletCollision(),
                new SScreenVisualLevelTimeLeft(),
                new SStateMachineUpdate(),
                new SAnimator(),
                new SScreenVisualAmmunitionCount(),
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
                new SScreenBloodEffect(),
                new SCameraShake(),
                new SScreenLoseAnimation(),
                new SScreenVisualReloadingWeapon(),
                new SSettingsButton(),
                new SPause(),
                new SScreenWinAnimation(),
                new SHapticController(),
                new SHapticButton(),
                new SToggleController(),
                new SSettingsMediator(),
                new SMoneyLootProvider(),
                new SMoneyLootUpdate(),
                new SDailyTaskUpdate(),
                new STargetWeaponUpdate(),
                new SWallInitialize(),
            };

            return systems;
        }

        SystemBase[] ISystemFactory.CreatePreviewSystems()
        {
            SystemBase[] systems = 
            {
                new SShopCharacterPreview(),
                new SShopCharacterRenderer(),
                new SShopSwipeButtons(),
                new SShopMediator(),
                new SShopUpgradeButtonProvider(),
                new SShopUpgradeButton(),
                new SMoneyUpdate(),
                new SShopPrice(),
                new SShopBuyButton(),
                new SShopElementsChangeState(),
                new SHapticController(),
                new SHapticButton(),
                new SShopTaskUpdate(),
                new SShopTaskProvider(),
                new SDailyTaskUpdate(),
            };

            return systems;
        }
    }
}
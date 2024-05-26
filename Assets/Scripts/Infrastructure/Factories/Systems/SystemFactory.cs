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
        ISystem[] ISystemFactory.CreateGameSystems()
        {
            ISystem[] systems = 
            {
                new SGroundBuildNavMesh(),
                new SRadarDraw(),
                new SSelectMesh(),
                new SScreenVisualLevelGoal(),
                new SMoneyUpdate(),
                new SBulletMove(),
                new SBulletLifeTime(),
                new SBulletCollision(),
                new SScreenVisualCurrentLevel(),
                new SGroundMesh(),
                new SDamageCombatLogViewUpdate(),
                new SEnemyHealthProvider(),
                new SEnemyHealthUpdate(),
                new SScreenVisualCharacterHealth(),
                new SScreenVisualLevelTimeLeft(),
                new SStateMachineUpdate(),
                new SAnimator(),
                new SScreenVisualAmmunitionCount(),
                new SCharacterSpawner(),
                new SPointerArrowProvider(),
                new SPointerArrowUpdate(),
                new SDamageCombatLogViewProvider(),
                new SDamageCombatLogUpdate(),
                new SJoystickUpdate(),
                new SObjectPoolUpdate(),
                new SUnitSpawner(),
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
                new STurretSpawner(),
                new SMeleeWeaponSendCombatLog(),
                new SDestroyMeshEffect(),
                new SHealthRegeneration(),
                new SRegenerationHealthProvider(),
                new SRegenerationHealthUpdate(),
                new SExecuteWeaponAmmunition(),
            };

            return systems;
        }

        ISystem[] ISystemFactory.CreatePreviewSystems()
        {
            ISystem[] systems = 
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
                new SShopCharacterStats(),
            };

            return systems;
        }
    }
}
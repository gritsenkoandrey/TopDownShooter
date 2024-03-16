using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Factories.UI
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;
        private readonly IAssetService _assetService;
        private readonly IObjectResolver _objectResolver;

        public UIFactory(IStaticDataService staticDataService, ICameraService cameraService, IGuiService guiService, 
            IAssetService assetService, IObjectResolver objectResolver)
        {
            _staticDataService = staticDataService;
            _cameraService = cameraService;
            _guiService = guiService;
            _assetService = assetService;
            _objectResolver = objectResolver;
        }

        async UniTask<BaseScreen> IUIFactory.CreateScreen(ScreenType type)
        {
            _guiService.Pop();
            ScreenData data = _staticDataService.ScreenData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _cameraService.ActivateCamera(type);
            _guiService.Push(screen);
            return screen;
        }

        async UniTask<BaseScreen> IUIFactory.CreatePopUp(ScreenType type)
        {
            ScreenData data = _staticDataService.ScreenData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            BaseScreen screen = _objectResolver.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            _cameraService.ActivateCamera(type);
            _guiService.Push(screen);
            return screen;
        }

        async UniTask<CUpgradeButton> IUIFactory.CreateUpgradeButton(UpgradeButtonType type, Transform parent)
        {
            UpgradeButtonData data = _staticDataService.UpgradeButtonData(type);
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);
            CUpgradeButton button = Object.Instantiate(prefab, parent).GetComponent<CUpgradeButton>();
            button.SetUpgradeButtonType(data.UpgradeButtonType);
            button.SetBaseCost(data.BaseCost);
            return button;
        }

        async UniTask<CEnemyHealth> IUIFactory.CreateEnemyHealth(IEnemy enemy, Transform parent)
        {
            UiData data = _staticDataService.UiData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.EnemyHealthPrefabReference);
            CEnemyHealth enemyHealth = Object.Instantiate(prefab, parent).GetComponent<CEnemyHealth>();
            enemyHealth.Enemy.SetValueAndForceNotify(enemy);
            return enemyHealth;
        }

        async UniTask<CPointerArrow> IUIFactory.CreatePointerArrow(Transform parent)
        {
            UiData data = _staticDataService.UiData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PointerArrowPrefabReference);
            CPointerArrow pointerArrow = Object.Instantiate(prefab, parent).GetComponent<CPointerArrow>();
            return pointerArrow;
        }

        async UniTask<CDamageCombatLogView> IUIFactory.CreateDamageView(Transform parent)
        {
            UiData data = _staticDataService.UiData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.DamageViewPrefabReference);
            CDamageCombatLogView damageCombatLogView = Object.Instantiate(prefab, parent).GetComponent<CDamageCombatLogView>();
            return damageCombatLogView;
        }
        
        async UniTask<CMoneyLoot> IUIFactory.CreateMoneyLoot(Transform parent)
        {
            UiData data = _staticDataService.UiData();
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.MoneyLootPrefabReference);
            CMoneyLoot damageCombatLogView = Object.Instantiate(prefab, parent).GetComponent<CMoneyLoot>();
            return damageCombatLogView;
        }
    }
}
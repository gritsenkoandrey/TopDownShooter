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

namespace CodeBase.Infrastructure.Factories.UI
{
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    public sealed class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICameraService _cameraService;
        private readonly IGuiService _guiService;
        private readonly IAssetService _assetService;
        
        private BaseScreen _currentScreen;

        public UIFactory(IStaticDataService staticDataService, ICameraService cameraService, IGuiService guiService, 
            IAssetService assetService)
        {
            _staticDataService = staticDataService;
            _cameraService = cameraService;
            _guiService = guiService;
            _assetService = assetService;
        }

        async UniTask<BaseScreen> IUIFactory.CreateScreen(ScreenType type)
        {
            DestroyCurrentScreen();
            
            ScreenData data = _staticDataService.ScreenData(type);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            _currentScreen = Object.Instantiate(prefab, _guiService.StaticCanvas.transform).GetComponent<BaseScreen>();
            
            _cameraService.ActivateCamera(type);

            return _currentScreen;
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

        async UniTask<CDamageView> IUIFactory.CreateDamageView(Transform parent)
        {
            UiData data = _staticDataService.UiData();

            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.DamageViewPrefabReference);

            CDamageView damageView = Object.Instantiate(prefab, parent).GetComponent<CDamageView>();

            return damageView;
        }

        void IUIFactory.CleanUp()
        {
            DestroyCurrentScreen();
        }

        private void DestroyCurrentScreen()
        {
            if (_currentScreen != null)
            {
                Object.Destroy(_currentScreen.gameObject);
                
                _currentScreen = null;
            }
        }
    }
}
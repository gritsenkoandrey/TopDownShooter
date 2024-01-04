using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Infrastructure.Factories.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICameraService _cameraService;
        private readonly IObjectResolver _objectResolver;
        private readonly IGuiService _guiService;
        private readonly IAssetService _assetService;

        public IList<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public IList<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();
        
        private BaseScreen _currentScreen;

        public UIFactory(IStaticDataService staticDataService, ICameraService cameraService, 
            IObjectResolver objectResolver, IGuiService guiService, IAssetService assetService)
        {
            _staticDataService = staticDataService;
            _cameraService = cameraService;
            _objectResolver = objectResolver;
            _guiService = guiService;
            _assetService = assetService;
        }

        async UniTask<BaseScreen> IUIFactory.CreateScreen(ScreenType type)
        {
            DestroyCurrentScreen();
            
            ScreenData data = _staticDataService.ScreenData(type);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            _currentScreen = _objectResolver
                .Instantiate(prefab, _guiService.StaticCanvas.transform)
                .GetComponent<BaseScreen>();
            
            _cameraService.ActivateCamera(type);

            return _currentScreen;
        }

        async UniTask<CUpgradeButton> IUIFactory.CreateUpgradeButton(UpgradeButtonType type, Transform parent)
        {
            UpgradeButtonData data = _staticDataService.UpgradeButtonData(type);
            
            GameObject prefab = await _assetService.LoadFromAddressable<GameObject>(data.PrefabReference);

            CUpgradeButton button = Object.Instantiate(prefab, parent).GetComponent<CUpgradeButton>();

            button.UpgradeButtonType = data.UpgradeButtonType;
            button.BaseCost = data.BaseCost;

            Registered(button);

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

        void IUIFactory.CleanUp()
        {
            DestroyCurrentScreen();
            
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Registered(IProgress progress)
        {
            if (progress is IProgressWriter writer)
            {
                ProgressWriters.Add(writer);
            }

            if (progress is IProgressReader reader)
            {
                ProgressReaders.Add(reader);
            }
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
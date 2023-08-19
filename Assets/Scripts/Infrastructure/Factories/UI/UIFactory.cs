using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI.Screens;
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

        public IList<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public IList<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();
        
        private BaseScreen _currentScreen;

        public UIFactory(IStaticDataService staticDataService, ICameraService cameraService, 
            IObjectResolver objectResolver, IGuiService guiService)
        {
            _staticDataService = staticDataService;
            _cameraService = cameraService;
            _objectResolver = objectResolver;
            _guiService = guiService;
        }

        BaseScreen IUIFactory.CreateScreen(ScreenType type)
        {
            if (_currentScreen != null)
            {
                Object.Destroy(_currentScreen.gameObject);
            }

            ScreenData screenData = _staticDataService.ScreenData(type);

            _currentScreen = _objectResolver.Instantiate(screenData.Prefab, _guiService.StaticCanvas.transform);
            
            _cameraService.ActivateCamera(type);

            return _currentScreen;
        }

        CUpgradeButton IUIFactory.CreateUpgradeButton(UpgradeButtonType type, Transform parent)
        {
            UpgradeButtonData data = _staticDataService.UpgradeButtonData(type);

            CUpgradeButton button = Object.Instantiate(data.Prefab, parent);

            button.UpgradeButtonType = data.UpgradeButtonType;
            button.BaseCost = data.BaseCost;

            Registered(button);

            return button;
        }

        CEnemyHealth IUIFactory.CreateEnemyHealth(IEnemy enemy, Transform parent)
        {
            UiData data = _staticDataService.UiData();
            
            CEnemyHealth enemyHealth = Object.Instantiate(data.EnemyHealth, parent);

            enemyHealth.Enemy.SetValueAndForceNotify(enemy);

            return enemyHealth;
        }

        void IUIFactory.CleanUp()
        {
            if (_currentScreen != null)
            {
                Object.Destroy(_currentScreen.gameObject);
                
                _currentScreen = null;
            }
            
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
    }
}
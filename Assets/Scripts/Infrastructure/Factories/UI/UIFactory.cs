using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;
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
        
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();
        
        private BaseScreen _currentScreen;
        private StaticCanvas _currentCanvas;

        public UIFactory(IStaticDataService staticDataService, ICameraService cameraService, IObjectResolver objectResolver)
        {
            _staticDataService = staticDataService;
            _cameraService = cameraService;
            _objectResolver = objectResolver;
        }

        StaticCanvas IUIFactory.CreateCanvas()
        {
            return _currentCanvas = Object.Instantiate(_staticDataService.StaticCanvasData());
        }

        BaseScreen IUIFactory.CreateScreen(ScreenType type)
        {
            if (_currentScreen != null)
            {
                Object.Destroy(_currentScreen.gameObject);
            }

            ScreenData screenData = _staticDataService.ScreenData(type);

            _currentScreen = _objectResolver.Instantiate(screenData.Prefab, _currentCanvas.transform);
            
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

        void IUIFactory.CleanUp()
        {
            if (_currentScreen != null)
            {
                Object.Destroy(_currentScreen.gameObject);
                
                _currentScreen = null;
            }

            if (_currentCanvas != null)
            {
                Object.Destroy(_currentCanvas.gameObject);
                
                _currentCanvas = null;
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
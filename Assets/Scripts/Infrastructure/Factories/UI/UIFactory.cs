using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.CameraMain;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;
using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ICameraService _cameraService;
        private readonly IGameStateService _gameStateService;
        
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();
        private BaseScreen CurrentScreen { get; set; }
        private StaticCanvas CurrentCanvas { get; set; }

        public UIFactory(IStaticDataService staticDataService, ICameraService cameraService, IGameStateService gameStateService)
        {
            _staticDataService = staticDataService;
            _cameraService = cameraService;
            _gameStateService = gameStateService;
        }

        StaticCanvas IUIFactory.CreateCanvas()
        {
            return CurrentCanvas = Object.Instantiate(_staticDataService.StaticCanvasData());
        }

        BaseScreen IUIFactory.CreateScreen(ScreenType type)
        {
            if (CurrentScreen != null)
            {
                Object.Destroy(CurrentScreen.gameObject);
            }

            ScreenData screenData = _staticDataService.ScreenData(type);

            CurrentScreen = Object.Instantiate(screenData.Prefab, CurrentCanvas.transform);
            
            CurrentScreen.Construct(this, _gameStateService);
            
            _cameraService.ActivateCamera(type);

            return CurrentScreen;
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
            if (CurrentScreen != null)
            {
                Object.Destroy(CurrentScreen.gameObject);
                
                CurrentScreen = null;
            }

            if (CurrentCanvas != null)
            {
                Object.Destroy(CurrentCanvas.gameObject);
                
                CurrentCanvas = null;
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
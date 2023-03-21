using System.Collections.Generic;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Progress;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Factories.UI
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        
        public List<IProgressReader> ProgressReaders { get; } = new();
        public List<IProgressWriter> ProgressWriters { get; } = new();
        private BaseScreen CurrentScreen { get; set; }
        private StaticCanvas CurrentCanvas { get; set; }

        public UIFactory(IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        public StaticCanvas CreateCanvas()
        {
            return CurrentCanvas = Object.Instantiate(_staticDataService.StaticCanvasData());
        }

        public BaseScreen CreateScreen(ScreenType type)
        {
            if (CurrentScreen != null)
            {
                Object.Destroy(CurrentScreen.gameObject);
            }

            ScreenData screenData = _staticDataService.ScreenData(type);

            CurrentScreen = Object.Instantiate(screenData.Prefab, CurrentCanvas.transform);
            
            CurrentScreen.Construct(this, _gameStateMachine);

            return CurrentScreen;
        }

        public CUpgradeButton CreateUpgradeButton(UpgradeButtonType type, Transform parent)
        {
            UpgradeButtonData data = _staticDataService.UpgradeButtonData(type);

            CUpgradeButton button = Object.Instantiate(data.Prefab, parent);

            button.UpgradeButtonType = data.UpgradeButtonType;
            button.BaseCost = data.BaseCost;

            Registered(button);

            return button;
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

        public void CleanUp()
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
    }
}
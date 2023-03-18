using CodeBase.Infrastructure.AssetData;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.UI.Factories
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAsset _asset;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticDataService;
        
        private BaseScreen CurrentScreen { get; set; }
        private GameObject CurrentCanvas { get; set; }

        public UIFactory(IAsset asset, IGameStateMachine gameStateMachine, IStaticDataService staticDataService)
        {
            _asset = asset;
            _gameStateMachine = gameStateMachine;
            _staticDataService = staticDataService;
        }

        public GameObject CreateCanvas()
        {
            return CurrentCanvas = Object.Instantiate(_asset.UiAssetData.Canvas);
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
        }
    }
}
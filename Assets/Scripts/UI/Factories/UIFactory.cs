using CodeBase.Infrastructure.Data;
using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.UI.Factories
{
    public sealed class UIFactory : IUIFactory
    {
        private readonly IAsset _asset;
        private readonly IGameStateMachine _gameStateMachine;
        
        public BaseScreen CurrentScreen { get; private set; }
        public GameObject CurrentCanvas { get; private set; }

        public UIFactory(IAsset asset, IGameStateMachine gameStateMachine)
        {
            _asset = asset;
            _gameStateMachine = gameStateMachine;
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

            CurrentScreen = null;

            switch (type)
            {
                case ScreenType.Lobby:
                    CurrentScreen = Object.Instantiate(_asset.UiAssetData.LobbyScreen, CurrentCanvas.transform);
                    CurrentScreen.Construct(this, _gameStateMachine);
                    break;
                case ScreenType.Game:
                    CurrentScreen = Object.Instantiate(_asset.UiAssetData.GameScreen, CurrentCanvas.transform);
                    CurrentScreen.Construct(this, _gameStateMachine);
                    break;
                case ScreenType.Result:
                    CurrentScreen = Object.Instantiate(_asset.UiAssetData.ResultScreen, CurrentCanvas.transform);
                    CurrentScreen.Construct(this, _gameStateMachine);
                    break;
            }

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
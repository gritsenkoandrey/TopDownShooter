using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private Button _button;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(StartGame);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _button.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            UIFactory.CreateScreen(ScreenType.Game);
        }
    }
}
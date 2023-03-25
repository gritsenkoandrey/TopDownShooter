using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LobbyScreen : BaseScreen
    {
        [SerializeField] private Button _button;

        protected override void OnEnable()
        {
            base.OnEnable();

            _button
                .OnClickAsObservable()
                .Subscribe(StartGame)
                .AddTo(this);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private void StartGame(Unit _)
        {
            UIFactory.CreateScreen(ScreenType.Game);
        }
    }
}
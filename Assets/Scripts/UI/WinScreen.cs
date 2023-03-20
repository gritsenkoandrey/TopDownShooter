using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public sealed class WinScreen : BaseScreen
    {
        [SerializeField] private Button _button;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(NextGame);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _button.onClick.RemoveListener(NextGame);
        }

        private void NextGame()
        {
            GameStateMachine.Enter<LoadProgressState>();
        }
    }
}
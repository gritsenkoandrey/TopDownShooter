using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public sealed class ResultScreen : BaseScreen
    {
        [SerializeField] private Button _button;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(RestartGame);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            
            _button.onClick.RemoveListener(RestartGame);
        }

        private void RestartGame()
        {
            GameStateMachine.Enter<LoadProgressState>();
        }
    }
}
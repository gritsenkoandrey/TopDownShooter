using CodeBase.Infrastructure.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Screens
{
    public sealed class LoseScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _splat;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(RestartGame);

            ShowLoseScreen();
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

        private void ShowLoseScreen()
        {
            _button.gameObject.SetActive(false);
            _splat.localScale = Vector3.zero;
            _splat
                .DOScale(Vector3.one * 2f, 1f)
                .SetEase(Ease.OutBack)
                .OnComplete(PunchButton);
        }

        private void PunchButton()
        {
            _button.gameObject.SetActive(true);
            _button.transform.DOPunchScale(Vector3.one * 0.15f, 0.25f, 5);
        }
    }
}
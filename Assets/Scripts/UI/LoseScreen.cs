using System.Collections;
using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public sealed class LoseScreen : BaseScreen
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _splat;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            _button.onClick.AddListener(RestartGame);

            StartCoroutine(ShowLoseScreen());
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

        private IEnumerator ShowLoseScreen()
        {
            float scale = 0f;
            
            _button.gameObject.SetActive(false);
            _splat.localScale = Vector3.one * scale;

            while (scale < 2f)
            {
                yield return null;

                scale += Time.deltaTime * 2.5f;

                _splat.localScale = Vector3.one * scale;
            }
            
            _button.gameObject.SetActive(true);
        }
    }
}
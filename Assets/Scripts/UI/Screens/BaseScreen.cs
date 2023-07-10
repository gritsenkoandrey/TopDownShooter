using CodeBase.Infrastructure.States;
using DG.Tweening;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private SafeArea _safeArea;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        protected IGameStateService GameStateService { get; private set; }

        [Inject]
        public void Construct(IGameStateService gameStateService)
        {
            GameStateService = gameStateService;
        }

        protected virtual void OnEnable() => _safeArea.ApplySafeArea();
        protected virtual void OnDisable() { }

        protected virtual Tween FadeCanvas(float from, float to, float duration)
        {
            return _canvasGroup.DOFade(to, duration).From(from).SetEase(Ease.Linear);
        }
    }
}
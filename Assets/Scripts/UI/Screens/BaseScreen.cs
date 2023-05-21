using CodeBase.Infrastructure.States;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        [SerializeField] private SafeArea _safeArea;
        
        protected IGameStateService GameStateService { get; private set; }

        [Inject]
        public void Construct(IGameStateService gameStateService)
        {
            GameStateService = gameStateService;
        }

        protected virtual void OnEnable() => _safeArea.ApplySafeArea();
        protected virtual void OnDisable() { }
    }
}
using CodeBase.Infrastructure.States;
using UnityEngine;
using VContainer;

namespace CodeBase.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        protected IGameStateService GameStateService { get; private set; }

        [Inject]
        public void Construct(IGameStateService gameStateService)
        {
            GameStateService = gameStateService;
        }

        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
    }
}
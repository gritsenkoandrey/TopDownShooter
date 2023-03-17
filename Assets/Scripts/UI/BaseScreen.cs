using CodeBase.Infrastructure.States;
using CodeBase.UI.Factories;
using UnityEngine;

namespace CodeBase.UI
{
    public abstract class BaseScreen : MonoBehaviour
    {
        protected IUIFactory UIFactory { get; private set; }
        protected IGameStateMachine GameStateMachine { get; private set; }

        public virtual void Construct(IUIFactory uiFactory, IGameStateMachine gameStateMachine)
        {
            UIFactory = uiFactory;
            GameStateMachine = gameStateMachine;
        }

        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
    }
}
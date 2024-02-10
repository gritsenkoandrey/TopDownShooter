using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.UI
{
    public sealed class StaticCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Canvas Canvas => _canvas;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public ScreenType CurrentScreenType { get; private set; }

        public void SetScreenType(ScreenType screenType) => CurrentScreenType = screenType;
    }
}
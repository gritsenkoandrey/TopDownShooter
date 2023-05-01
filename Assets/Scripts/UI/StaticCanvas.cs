using UnityEngine;

namespace CodeBase.UI
{
    public sealed class StaticCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;

        public Canvas Canvas => _canvas;
        public CanvasGroup CanvasGroup => _canvasGroup;
    }
}
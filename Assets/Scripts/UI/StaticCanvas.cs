using UnityEngine;

namespace CodeBase.UI
{
    public sealed class StaticCanvas : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public Canvas Canvas => _canvas;
    }
}
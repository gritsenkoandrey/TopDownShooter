using UnityEngine;

namespace CodeBase.UI
{
    public sealed class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public void ApplySafeArea()
        {
            Rect rect = Screen.safeArea;
            
            Vector2 anchorMin = rect.position;
            Vector2 anchorMax = rect.position + rect.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}
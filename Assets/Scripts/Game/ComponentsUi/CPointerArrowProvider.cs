using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CPointerArrowProvider : EntityComponent<CPointerArrowProvider>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _offset;
        
        public Rect Rect => _rectTransform.rect;
        public float Offset => _offset;
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CPointerArrow : EntityComponent<CPointerArrow>
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CanvasGroup _canvasGroup;

        public RectTransform RectTransform => _rectTransform;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Rect Rect { get; private set; }
        public IEnemy Target { get; private set; }
        public float Offset { get; private set; }
        
        public void SetTarget(IEnemy target) => Target = target;
        public void SetRectProvider(Rect rect) => Rect = rect;
        public void SetOffset(float offset) => Offset = offset;
    }
}
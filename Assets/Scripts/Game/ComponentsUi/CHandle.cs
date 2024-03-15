using CodeBase.ECSCore;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CHandle : EntityComponent<CHandle>, IDragHandler, IEndDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private GameObject _imageOn;
        [SerializeField] private GameObject _imageOff;
        
        public float Width => _rectTransform.rect.width;
        public float X => transform.localPosition.x;

        public IReactiveCommand<PointerEventData> OnDrag { get; } = new ReactiveCommand<PointerEventData>();
        public IReactiveCommand<Unit> OnEndDrag { get; } = new ReactiveCommand();

        public void IsActive(bool isActive)
        {
            _imageOn.SetActive(isActive);
            _imageOff.SetActive(!isActive);
        }

        void IDragHandler.OnDrag(PointerEventData eventData) => OnDrag.Execute(eventData);
        
        void IEndDragHandler.OnEndDrag(PointerEventData eventData) => OnEndDrag.Execute(Unit.Default);
    }
}
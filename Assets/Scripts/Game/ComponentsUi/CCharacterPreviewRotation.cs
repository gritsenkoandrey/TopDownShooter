using CodeBase.ECSCore;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewRotation : EntityComponent<CCharacterPreviewRotation>, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform _model;
        
        public Transform Model => _model;
        public readonly ReactiveCommand<PointerEventData> OnTouch = new();
        public readonly ReactiveCommand OnStartTouch = new();
        public readonly ReactiveCommand OnEndTouch = new();
        public Tween Tween { get; set; }

        void IDragHandler.OnDrag(PointerEventData eventData) => OnTouch.Execute(eventData);
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) => OnStartTouch.Execute();
        void IEndDragHandler.OnEndDrag(PointerEventData eventData) => OnEndTouch.Execute();
    }
}
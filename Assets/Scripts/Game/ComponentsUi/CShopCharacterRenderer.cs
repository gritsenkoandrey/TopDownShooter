using CodeBase.ECSCore;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShopCharacterRenderer : EntityComponent<CShopCharacterRenderer>, 
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private RawImage _rawImage;

        public RawImage RawImage => _rawImage;
        public ReactiveCommand<PointerEventData> OnTouch { get; } = new();
        public ReactiveCommand OnStartTouch { get; } = new();
        public ReactiveCommand OnEndTouch { get; } = new();
        public Tween Tween { get; set; }

        void IDragHandler.OnDrag(PointerEventData eventData) => OnTouch.Execute(eventData);
        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData) => OnStartTouch.Execute();
        void IEndDragHandler.OnEndDrag(PointerEventData eventData) => OnEndTouch.Execute();
    }
}
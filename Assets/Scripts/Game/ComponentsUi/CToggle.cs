using CodeBase.ECSCore;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CToggle : EntityComponent<CToggle>, IPointerClickHandler
    {
        [SerializeField] private CHandle _handle;

        [SerializeField] private RectTransform _container;
        [SerializeField] private Image _containerImage;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;
        [SerializeField] private float _offset;

        public CHandle Handle => _handle;
        public float Offset { get; private set; }
        public Tween Tween { get; set; }

        public IReactiveProperty<bool> IsEnable { get; } = new ReactiveProperty<bool>(true);
        public IReactiveCommand<Unit> OnClick { get; } = new ReactiveCommand();

        public void IsActive(bool isActive) => _containerImage.color = isActive ? _activeColor : _inactiveColor;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) => OnClick.Execute(Unit.Default);

        protected override void OnEntityCreate()
        {
            base.OnEntityCreate();
            
            SetOffset();
        }

        private void SetOffset() => Offset = _container.rect.width / 2f - _handle.Width / 2f - _offset;
    }
}
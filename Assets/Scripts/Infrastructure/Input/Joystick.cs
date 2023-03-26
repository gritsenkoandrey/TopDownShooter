using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Infrastructure.Input
{
    public sealed class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IInput
    {
        [SerializeField] private RectTransform _movementArea;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private RectTransform _thumb;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _valueMultiplier = 1f;
        [SerializeField] private float _movementAreaRadius = 75f;
        [SerializeField] private float _deadZoneRadius = 0f;
        [SerializeField] private bool _isStatick = false;

        private const float Offset = 4f;
        
        private Vector3 _startPosition;
        private float _loverMovementAreaRadius;
        private float _movementAreaRadiusSqr;
        private float _deadZoneAreaRadiusSqr;
        private float _opacity;
        private bool _joystickHeld;

        public Vector2 Vector { get; private set; }

        private void Awake()
        {
            Vector = Vector2.zero;

            _opacity = 0f;
            _canvasGroup.alpha = _opacity;
            _startPosition = _handle.position;
            _loverMovementAreaRadius = 1f / _movementAreaRadius;
            _movementAreaRadiusSqr = Mathf.Pow(_movementAreaRadius, 2f);
            _deadZoneAreaRadiusSqr = Mathf.Pow(_deadZoneRadius, 2f);
        }

        public void OnUpdate()
        {
            _opacity = _joystickHeld ? 
                Mathf.Min(1f, _opacity + Time.deltaTime * Offset) : 
                Mathf.Max(0f, _opacity - Time.deltaTime * Offset);

            _canvasGroup.alpha = _opacity;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _joystickHeld = true;

            if (_isStatick)
            {
                _handle.position = _startPosition;
            }
            else
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle
                    (_movementArea, eventData.position, eventData.pressEventCamera, out Vector3 position);

                _handle.position = position;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (_handle, eventData.position, eventData.pressEventCamera, out Vector2 direction);

            if (direction.sqrMagnitude < _deadZoneAreaRadiusSqr)
            {
                Vector = Vector2.zero;
            }
            else
            {
                if (direction.sqrMagnitude > _movementAreaRadiusSqr)
                {
                    direction = direction.normalized * _movementAreaRadius;
                }

                Vector = direction * _loverMovementAreaRadius * _valueMultiplier;
            }
            
            _thumb.localPosition = direction;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickHeld = false;
            _handle.position = _startPosition;
            _thumb.localPosition = Vector3.zero;

            Vector = Vector2.zero;
        }
    }
}
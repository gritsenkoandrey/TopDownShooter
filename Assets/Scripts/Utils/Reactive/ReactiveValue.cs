using System;
using UnityEngine;

namespace CodeBase.Utils.Reactive
{
    [Serializable]
    public sealed class ReactiveValue<T>
    {
        [SerializeField] private T _value;
        
        private Action<T> _onValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                _onValueChanged?.Invoke(value);
            }
        }
        
        public void Subscribe(Action<T> onAction)
        {
            _onValueChanged += onAction;
        }
        
        public void Unsubscribe(Action<T> onAction)
        {
            _onValueChanged -= onAction;
        }
    }
}
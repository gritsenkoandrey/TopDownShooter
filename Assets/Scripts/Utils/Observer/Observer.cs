using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Utils.Observer
{
    public sealed class Observer<T> : IObserver<T>
    {
        private T _value;
        private Action<T> _onNext;
        private Func<T, bool> _onWhere;
        private readonly Subscriber<T> _subscriber;
        private readonly IEqualityComparer<T> _equalityComparer;
        private bool _skipInitialValue;

        public T Value
        {
            get => _value;
            
            set
            {
                if (!_equalityComparer.Equals(_value, value))
                {
                    SetValue(value);
                }
            }
        }

        public Observer(T value = default)
        {
            _value = value;
            _subscriber = new Subscriber<T>();
            _equalityComparer = EqualityComparer<T>.Default;
        }

        public IDisposable Subscribe(Action<T> onNext)
        {
            _onNext = onNext;

            if (!_skipInitialValue && !_equalityComparer.Equals(_value, default))
            {
                SetValue(_value);
            }
            
            return _subscriber.Subscribe(this);
        }

        public Observer<T> SkipInitializeValue()
        {
            _skipInitialValue = true;
            
            return this;
        }

        public Observer<T> Where(Func<T, bool> onWhere)
        {
            _onWhere = onWhere;
            
            return this;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            Debug.LogError(error);
        }

        public void OnNext(T value)
        {
            _value = value;
        }

        private void SetValue(T value)
        {
            _value = value;
            
            if (_onWhere?.Invoke(_value) is false)
            {
                return;
            }

            _subscriber.Execute(_value);
            _onNext?.Invoke(_value);
        }
    }
}
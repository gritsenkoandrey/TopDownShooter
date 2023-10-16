using System;

namespace CodeBase.Utils.Reactive
{
    [Serializable]
    public sealed class ReactiveEvent<T>
    {
        private Action<T> _onAction;

        public void Invoke(T obj)
        {
            _onAction?.Invoke(obj);
        }

        public void Subscribe(Action<T> onAction)
        {
            _onAction += onAction;
        }
        
        public void Unsubscribe(Action<T> onAction)
        {
            _onAction -= onAction;
        }
    }
}
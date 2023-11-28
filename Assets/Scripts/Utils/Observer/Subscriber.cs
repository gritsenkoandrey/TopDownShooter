using System;
using System.Collections.Generic;

namespace CodeBase.Utils.Observer
{
    public sealed class Subscriber<T> : IObservable<T>
    {
        private readonly IList<IObserver<T>> _observers;

        public Subscriber()
        {
            _observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observers.Add(observer);

            return new UnSubscriber<T>(_observers, observer);
        }

        public void Execute(T value)
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].OnNext(value);
            }
        }
    }
}
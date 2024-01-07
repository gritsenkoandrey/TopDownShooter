using System;
using UniRx;

namespace CodeBase.Infrastructure.Progress
{
    public interface IData<T> : IDisposable
    {
        public IReactiveProperty<T> Data { get; }
    }
}
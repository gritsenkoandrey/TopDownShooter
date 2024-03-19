using System;
using UniRx;

namespace CodeBase.Infrastructure.Progress
{
    public interface IData<T> : IDisposable
    {
        IReactiveProperty<T> Data { get; }
    }
}
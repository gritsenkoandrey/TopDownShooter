using UniRx;
using UnityEngine;

namespace CodeBase.ECSCore
{
    public abstract class Entity : MonoBehaviour
    {
        public CompositeDisposable LifetimeDisposable { get; private set; }

        protected virtual void OnEntityCreate() => LifetimeDisposable = new CompositeDisposable();
        protected virtual void OnEntityEnable() { }
        protected virtual void OnEntityDisable() => LifetimeDisposable?.Clear();
        protected virtual void OnEntityDestroy() => LifetimeDisposable?.Dispose();
    }
}
using System;
using UniRx;

namespace CodeBase.ECSCore
{
    public abstract class SystemBase : ISystem
    {
        protected readonly CompositeDisposable LifetimeDisposable;
        
        protected SystemBase() => LifetimeDisposable = new CompositeDisposable();

        void ISystem.EnableSystem() => OnEnableSystem();
        void ISystem.DisableSystem() => OnDisableSystem();
        void ISystem.Update() => OnUpdate();
        void ISystem.FixedUpdate() => OnFixedUpdate();
        void ISystem.LateUpdate() => OnLateUpdate();
        void IDisposable.Dispose() => OnDispose();

        protected virtual void OnEnableSystem() { }
        protected virtual void OnDisableSystem() => LifetimeDisposable.Clear();
        protected virtual void OnUpdate() { }
        protected virtual void OnFixedUpdate() { }
        protected virtual void OnLateUpdate() { }
        protected virtual void OnDispose() => LifetimeDisposable?.Dispose();
    }
}
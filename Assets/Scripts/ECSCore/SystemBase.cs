using UniRx;

namespace CodeBase.ECSCore
{
    public abstract class SystemBase
    {
        protected readonly CompositeDisposable LifetimeDisposable;
        protected SystemBase() => LifetimeDisposable = new CompositeDisposable();

        public void EnableSystem() => OnEnableSystem();
        public void DisableSystem() => OnDisableSystem();
        public void Tick() => OnTick();

        protected virtual void OnEnableSystem() { }
        protected virtual void OnDisableSystem() => LifetimeDisposable.Clear();
        protected virtual void OnTick() { }
    }
}
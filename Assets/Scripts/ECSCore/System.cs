using UniRx;

namespace AndreyGritsenko.ECSCore
{
    public abstract class System
    {
        protected readonly CompositeDisposable LifetimeDisposable;
        protected System() => LifetimeDisposable = new CompositeDisposable();

        public void EnableSystem() => OnEnableSystem();
        public void DisableSystem() => OnDisableSystem();

        protected virtual void OnEnableSystem() { }
        protected virtual void OnDisableSystem() => LifetimeDisposable.Clear();
    }
}
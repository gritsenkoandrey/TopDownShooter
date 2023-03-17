using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SystemEntryPoint>(Lifetime.Scoped);
        }
    }
}
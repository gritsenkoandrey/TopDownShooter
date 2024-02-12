using CodeBase.LifeTime.Systems;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime.Scopes
{ 
    public sealed class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf();
        }
    }
}
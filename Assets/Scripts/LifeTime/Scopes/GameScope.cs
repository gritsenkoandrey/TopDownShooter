using CodeBase.Infrastructure.Models;
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

            builder.Register<PauseModel>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}
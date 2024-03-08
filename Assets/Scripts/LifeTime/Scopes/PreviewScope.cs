using System;
using CodeBase.Infrastructure.Models;
using CodeBase.LifeTime.Systems;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime.Scopes
{
    public sealed class PreviewScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<CharacterPreviewModel>(Lifetime.Scoped).As<IInitializable, IDisposable>().AsSelf();
            builder.Register<ShopModel>(Lifetime.Scoped).AsSelf();
            builder.RegisterEntryPoint<EntryPointPreviewSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}
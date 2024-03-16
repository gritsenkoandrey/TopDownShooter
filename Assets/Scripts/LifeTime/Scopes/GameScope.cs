using System;
using CodeBase.Infrastructure.Factories.Effects;
using CodeBase.Infrastructure.Factories.Weapon;
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
            builder.Register<LootModel>(Lifetime.Scoped).AsSelf();
            builder.Register<DamageCombatLog>(Lifetime.Scoped).As<IDisposable>().AsSelf();
            
            builder.Register<IWeaponFactory, WeaponFactory>(Lifetime.Scoped);
            builder.Register<IEffectFactory, EffectFactory>(Lifetime.Scoped);

            builder.RegisterEntryPoint<EntryPointGameSystem>(Lifetime.Scoped).AsSelf().Build();
        }
    }
}
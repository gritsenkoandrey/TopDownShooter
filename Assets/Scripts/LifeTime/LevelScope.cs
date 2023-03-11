using CodeBase.Game.Components;
using CodeBase.Infrastructure.Input;
using VContainer;
using VContainer.Unity;

namespace CodeBase.LifeTime
{
    public sealed class LevelScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<Joystick>();
            builder.RegisterComponentInHierarchy<CCharacter>();
            
            builder.RegisterEntryPoint<InitSystems>();
        }
    }
}
using AndreyGritsenko.Game.Components;
using SimpleInputNamespace;
using VContainer;
using VContainer.Unity;

namespace AndreyGritsenko.Infrastructure
{
    public class GameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<Joystick>();
            builder.RegisterComponentInHierarchy<CCharacter>();
            
            builder.RegisterEntryPoint<InitSystems>();
        }
    }
}
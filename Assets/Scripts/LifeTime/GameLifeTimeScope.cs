using AndreyGritsenko.Game.Components;
using AndreyGritsenko.Infrastructure.Input;
using VContainer;
using VContainer.Unity;

namespace AndreyGritsenko.LifeTime
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
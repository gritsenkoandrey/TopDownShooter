using AndreyGritsenko.ECSCore;
using AndreyGritsenko.Game.Components;

namespace AndreyGritsenko.Game.Systems
{
    public sealed class SGroundBuildNavMesh : SystemComponent<CGround>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CGround component)
        {
            base.OnEnableComponent(component);
            
            component.BuildNavMesh();
        }

        protected override void OnDisableComponent(CGround component)
        {
            base.OnDisableComponent(component);
        }
    }
}
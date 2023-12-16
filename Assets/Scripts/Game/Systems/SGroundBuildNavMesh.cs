using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.Systems
{
    public sealed class SGroundBuildNavMesh : SystemComponent<CGround>
    {
        protected override void OnEnableComponent(CGround component)
        {
            base.OnEnableComponent(component);
            
            component.BuildNavMesh();
        }
    }
}
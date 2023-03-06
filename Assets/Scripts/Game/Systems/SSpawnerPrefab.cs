using AndreyGritsenko.ECSCore;
using AndreyGritsenko.Game.Components;

namespace AndreyGritsenko.Game.Systems
{
    public sealed class SSpawnerPrefab : SystemComponent<CSpawner>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CSpawner component)
        {
            base.OnEnableComponent(component);

            component.CreatePrefab();
        }

        protected override void OnDisableComponent(CSpawner component)
        {
            base.OnDisableComponent(component);
        }
    }
}
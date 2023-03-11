using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.Systems
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
        
        protected override void OnTick()
        {
            base.OnTick();
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
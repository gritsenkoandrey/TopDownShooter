using CodeBase.ECSCore;
using CodeBase.Game.Components;

namespace CodeBase.Game.Systems
{
    public sealed class SBulletController : SystemComponent<CBullet>
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

        protected override void OnEnableComponent(CBullet component)
        {
            base.OnEnableComponent(component);
        }

        protected override void OnDisableComponent(CBullet component)
        {
            base.OnDisableComponent(component);
        }
    }
}
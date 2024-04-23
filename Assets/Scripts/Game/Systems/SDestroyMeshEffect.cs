using System;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UniRx;

namespace CodeBase.Game.Systems
{
    public sealed class SDestroyMeshEffect : SystemComponent<CDestroyMeshEffect>
    {
        protected override void OnEnableComponent(CDestroyMeshEffect component)
        {
            base.OnEnableComponent(component);
            
            component.OnInit
                .First(mesh => mesh != default)
                .Delay(TimeSpan.FromSeconds(1f))
                .Subscribe(component.Init)
                .AddTo(component.LifetimeDisposable);
        }

        protected override void OnDisableComponent(CDestroyMeshEffect component)
        {
            base.OnDisableComponent(component);

            component.OnInit.Value = default;
        }
    }
}
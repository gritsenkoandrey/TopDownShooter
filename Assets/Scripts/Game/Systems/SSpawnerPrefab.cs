using AndreyGritsenko.ECSCore;
using AndreyGritsenko.Game.Components;
using UnityEngine;

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

            Object.Instantiate(component.Prefab, component.transform.position, Quaternion.identity);
        }

        protected override void OnDisableComponent(CSpawner component)
        {
            base.OnDisableComponent(component);
        }
    }
}
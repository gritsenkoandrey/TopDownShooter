using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SSelectMesh : SystemComponent<CSelectMesh>
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

        protected override void OnEnableComponent(CSelectMesh component)
        {
            base.OnEnableComponent(component);
            
            SelectRandomMesh(component);
        }

        protected override void OnDisableComponent(CSelectMesh component)
        {
            base.OnDisableComponent(component);
        }

        private void SelectRandomMesh(CSelectMesh component)
        {
            int index = Random.Range(0, component.Meshes.Length);

            for (int i = 0; i < component.Meshes.Length; i++)
            {
                component.Meshes[i].SetActive(i == index);
            }
        }
    }
}
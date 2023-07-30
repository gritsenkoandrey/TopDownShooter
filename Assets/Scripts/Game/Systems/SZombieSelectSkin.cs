using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SZombieSelectSkin : SystemComponent<CSelectMesh>
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnEnableComponent(CSelectMesh component)
        {
            base.OnEnableComponent(component);
            
            SelectRandomSkin(component);
        }

        protected override void OnDisableComponent(CSelectMesh component)
        {
            base.OnDisableComponent(component);
        }

        private void SelectRandomSkin(CSelectMesh component)
        {
            int index = Random.Range(0, component.Meshes.Length);

            for (int i = 0; i < component.Meshes.Length; i++)
            {
                component.Meshes[i].SetActive(i == index);
            }
        }
    }
}
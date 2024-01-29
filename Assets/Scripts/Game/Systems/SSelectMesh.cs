using CodeBase.ECSCore;
using CodeBase.Game.Components;
using UnityEngine;

namespace CodeBase.Game.Systems
{
    public sealed class SSelectMesh : SystemComponent<CSelectMesh>
    {
        protected override void OnEnableComponent(CSelectMesh component)
        {
            base.OnEnableComponent(component);
            
            SelectRandomSkin(component);
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
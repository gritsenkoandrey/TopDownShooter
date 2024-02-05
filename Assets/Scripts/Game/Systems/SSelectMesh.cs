using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Utils;

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
            int index = component.Meshes.GetRandomIndex();

            for (int i = 0; i < component.Meshes.Length; i++)
            {
                component.Meshes[i].SetActive(i == index);
            }
        }
    }
}
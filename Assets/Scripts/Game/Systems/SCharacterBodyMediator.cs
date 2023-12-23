using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Infrastructure.Models;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterBodyMediator : SystemComponent<CCharacter>
    {
        private readonly InventoryModel _inventoryModel;

        public SCharacterBodyMediator(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }

        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);

            SetEquipment(component);
        }

        private void SetEquipment(CCharacter component)
        {
            for (int i = 0; i < component.BodyMediator.Bodies.Length; i++)
            {
                component.BodyMediator.Bodies[i].SetActive(_inventoryModel.EquipmentIndex.Value == i);
            }

            for (int i = 0; i < component.BodyMediator.Heads.Length; i++)
            {
                component.BodyMediator.Heads[i].SetActive(_inventoryModel.EquipmentIndex.Value == i);
            }
        }
    }
}
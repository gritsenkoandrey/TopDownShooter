using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterAmmunitionView : SystemComponent<CCharacterAmmunitionView>
    {
        private readonly InventoryModel _inventoryModel;

        public SCharacterAmmunitionView(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }
        
        protected override void OnEnableComponent(CCharacterAmmunitionView component)
        {
            base.OnEnableComponent(component);

            _inventoryModel.ClipCount
                .Subscribe(count => component.AmmunitionCount.text = count.ToString())
                .AddTo(component.LifetimeDisposable);
        }
    }
}
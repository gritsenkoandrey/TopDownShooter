using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterAmmunitionView : SystemComponent<CCharacterAmmunitionView>
    {
        private InventoryModel _inventoryModel;

        [Inject]
        private void Construct(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }
        
        protected override void OnEnableComponent(CCharacterAmmunitionView component)
        {
            base.OnEnableComponent(component);

            _inventoryModel.ClipCount
                .Subscribe(count =>
                {
                    string text = count > 0 ? count.ToString() : "R";
                    
                    component.AmmunitionCount.text = text + SpriteAssetExtension.Ammunition;
                })
                .AddTo(component.LifetimeDisposable);
        }
    }
}
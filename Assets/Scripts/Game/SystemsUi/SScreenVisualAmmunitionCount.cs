﻿using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SScreenVisualAmmunitionCount : SystemComponent<CCharacterAmmunitionView>
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

            void SetAmmunitionCountText(int count) => component.AmmunitionCount.text = count.ToString();

            _inventoryModel.ClipCount
                .Subscribe(SetAmmunitionCountText)
                .AddTo(component.LifetimeDisposable);
        }
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeShop : SystemComponent<CUpgradeShop>
    {
        private IUIFactory _uiFactory;
        
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();

            _uiFactory = AllServices.Container.Single<IUIFactory>();
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
        }

        protected override void OnTick()
        {
            base.OnTick();
        }

        protected override void OnEnableComponent(CUpgradeShop component)
        {
            base.OnEnableComponent(component);

            for (int i = 0; i < component.UpgradeButtonType.Length; i++)
            {
                _uiFactory.CreateUpgradeButton(component.UpgradeButtonType[i], component.Root);
            }
        }

        protected override void OnDisableComponent(CUpgradeShop component)
        {
            base.OnDisableComponent(component);
        }
    }
}
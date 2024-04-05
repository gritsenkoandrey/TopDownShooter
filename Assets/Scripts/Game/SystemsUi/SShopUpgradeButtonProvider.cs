using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Factories.UI;
using Cysharp.Threading.Tasks;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopUpgradeButtonProvider : SystemComponent<CShopUpgradeWindow>
    {
        private IUIFactory _uiFactory;

        [Inject]
        private void Construct(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        protected override void OnEnableComponent(CShopUpgradeWindow component)
        {
            base.OnEnableComponent(component);

            CreateUpgradeButtons(component).Forget();
        }
        
        private async UniTaskVoid CreateUpgradeButtons(CShopUpgradeWindow component)
        {
            for (int i = 0; i < component.UpgradeButtonType.Length; i++)
            {
                await _uiFactory.CreateUpgradeButton(component.UpgradeButtonType[i], component.Root.transform);
            }
        }
    }
}
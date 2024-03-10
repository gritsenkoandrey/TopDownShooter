using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Factories.UI;
using CodeBase.Infrastructure.Models;
using Cysharp.Threading.Tasks;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SUpgradeShop : SystemComponent<CUpgradeShop>
    {
        private IUIFactory _uiFactory;
        private CharacterPreviewModel _characterPreviewModel;

        [Inject]
        private void Construct(IUIFactory uiFactory, CharacterPreviewModel characterPreviewModel)
        {
            _uiFactory = uiFactory;
            _characterPreviewModel = characterPreviewModel;
        }

        protected override void OnEnableComponent(CUpgradeShop component)
        {
            base.OnEnableComponent(component);

            CreateUpgradeButtons(component).Forget();

            _characterPreviewModel.State
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case PreviewState.Start:
                        case PreviewState.BuyWeapon:
                        case PreviewState.BuySkin:
                            SetActiveCanvasGroup(component, false);
                            break;
                        case PreviewState.BuyUpgrades:
                            SetActiveCanvasGroup(component, true);
                            break;
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }
        
        private async UniTaskVoid CreateUpgradeButtons(CUpgradeShop component)
        {
            for (int i = 0; i < component.UpgradeButtonType.Length; i++)
            {
                await _uiFactory.CreateUpgradeButton(component.UpgradeButtonType[i], component.Root.transform);
            }
        }
        
        private void SetActiveCanvasGroup(CUpgradeShop component, bool isActive)
        {
            component.CanvasGroup.alpha = isActive ? 1f : 0f;
            component.CanvasGroup.interactable = isActive;
            component.CanvasGroup.blocksRaycasts = isActive;
        }
    }
}
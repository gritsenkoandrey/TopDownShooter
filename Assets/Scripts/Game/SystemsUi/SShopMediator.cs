using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopMediator : SystemComponent<CShop>
    {
        private CharacterPreviewModel _characterPreviewModel;
        private IProgressService _progressService;

        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel, IProgressService progressService)
        {
            _characterPreviewModel = characterPreviewModel;
            _progressService = progressService;
        }

        protected override void OnEnableComponent(CShop component)
        {
            base.OnEnableComponent(component);

            component.StartButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ => _characterPreviewModel.PlayPreviewAnimation())
                .AddTo(component.LifetimeDisposable);

            _characterPreviewModel.State
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case PreviewState.Start:
                            SetActiveShopElementsCanvasGroup(component, true);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveTaskProviderCanvasGroup(component, false);
                            SetActiveBuyButton(component, false);
                            SetActiveBackButton(component, false);
                            SetActiveSwipeButtons(component, false);
                            SetActiveStartButton(component, true);
                            break;
                        case PreviewState.BuyWeapon:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveTaskProviderCanvasGroup(component, false);
                            SetActiveBuyButton(component, true);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, true);
                            SetActiveStartButton(component, false);
                            break;
                        case PreviewState.BuySkin:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveTaskProviderCanvasGroup(component, false);
                            SetActiveBuyButton(component, true);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, true);
                            SetActiveStartButton(component, false);
                            break;
                        case PreviewState.BuyUpgrades:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, true);
                            SetActiveTaskProviderCanvasGroup(component, false);
                            SetActiveBuyButton(component, false);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, false);
                            SetActiveStartButton(component, false);
                            break;
                        case PreviewState.DailyTask:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveTaskProviderCanvasGroup(component, true);
                            SetActiveBuyButton(component, false);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, false);
                            SetActiveStartButton(component, false);
                            break;
                    }
                })
                .AddTo(component.LifetimeDisposable);
            
            _progressService.MoneyData.Data
                .Pairwise()
                .Where(money => money.Previous > money.Current)
                .Subscribe(_ => _characterPreviewModel.PlayAnimation(Animations.Victory))
                .AddTo(component.LifetimeDisposable);
        }

        private void SetActiveShopElementsCanvasGroup(CShop component, bool isActive)
        {
            component.ShopElements.ShopButtonsCanvasGroup.alpha = isActive ? 1f : 0f;
            component.ShopElements.ShopButtonsCanvasGroup.interactable = isActive;
            component.ShopElements.ShopButtonsCanvasGroup.blocksRaycasts = isActive;
        }
        
        private void SetActiveUpgradeWindowCanvasGroup(CShop component, bool isActive)
        {
            component.UpgradeWindow.CanvasGroup.alpha = isActive ? 1f : 0f;
            component.UpgradeWindow.CanvasGroup.interactable = isActive;
            component.UpgradeWindow.CanvasGroup.blocksRaycasts = isActive;
        }

        private void SetActiveTaskProviderCanvasGroup(CShop component, bool isActive)
        {
            component.TaskProvider.CanvasGroup.alpha = isActive ? 1f : 0f;
            component.TaskProvider.CanvasGroup.interactable = isActive;
            component.TaskProvider.CanvasGroup.blocksRaycasts = isActive;
        }

        private void SetActiveBuyButton(CShop component, bool isActive)
        {
            component.BuyButton.Button.gameObject.SetActive(isActive);
        }

        private void SetActiveBackButton(CShop component, bool isActive)
        {
            component.ShopElements.BackButton.gameObject.SetActive(isActive);
        }

        private void SetActiveSwipeButtons(CShop component, bool isActive)
        {
            component.SwipeButtons.LeftButton.gameObject.SetActive(isActive);
            component.SwipeButtons.RightButton.gameObject.SetActive(isActive);
        }

        private void SetActiveStartButton(CShop component, bool isActive)
        {
            component.StartButton.gameObject.SetActive(isActive);
        }
    }
}
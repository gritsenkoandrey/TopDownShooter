using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopMediator : SystemComponent<CShop>
    {
        private CharacterPreviewModel _characterPreviewModel;
        
        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel)
        {
            _characterPreviewModel = characterPreviewModel;
        }

        protected override void OnEnableComponent(CShop component)
        {
            base.OnEnableComponent(component);

            component.StartButton
                .OnClickAsObservable()
                .First()
                .Subscribe(_ =>
                {
                    _characterPreviewModel.CharacterPreview.CharacterPreviewAnimator.Animator
                        .SetFloat(Animations.PreviewBlend, UnityEngine.Random.Range(0, 4));
                    _characterPreviewModel.CharacterPreview.CharacterPreviewAnimator.Animator
                        .SetTrigger(Animations.Preview);
                })
                .AddTo(component.LifetimeDisposable);

            _characterPreviewModel.State
                .Subscribe(state =>
                {
                    switch (state)
                    {
                        case PreviewState.Start:
                            SetActiveShopElementsCanvasGroup(component, true);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveBuyButton(component, false);
                            SetActiveBackButton(component, false);
                            SetActiveSwipeButtons(component, false);
                            SetActiveStartButton(component, true);
                            break;
                        case PreviewState.BuyWeapon:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveBuyButton(component, true);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, true);
                            SetActiveStartButton(component, false);
                            break;
                        case PreviewState.BuySkin:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, false);
                            SetActiveBuyButton(component, true);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, true);
                            SetActiveStartButton(component, false);
                            break;
                        case PreviewState.BuyUpgrades:
                            SetActiveShopElementsCanvasGroup(component, false);
                            SetActiveUpgradeWindowCanvasGroup(component, true);
                            SetActiveBuyButton(component, false);
                            SetActiveBackButton(component, true);
                            SetActiveSwipeButtons(component, false);
                            SetActiveStartButton(component, false);
                            break;
                    }
                })
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
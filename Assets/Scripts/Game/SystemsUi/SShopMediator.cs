using System;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SShopMediator : SystemComponent<CShop>
    {
        private CharacterPreviewModel _characterPreviewModel;

        private const float DelayClick = 0.25f;
        
        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel)
        {
            _characterPreviewModel = characterPreviewModel;
        }

        protected override void OnEnableComponent(CShop component)
        {
            base.OnEnableComponent(component);

            component.WeaponShopButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(DelayClick))
                .Subscribe(_ => ChangeState(component.WeaponShopButton, PreviewState.BuyWeapon).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.SkinShopButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(DelayClick))
                .Subscribe(_ => ChangeState(component.SkinShopButton, PreviewState.BuySkin).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.UpgradeShopButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(DelayClick))
                .Subscribe(_ => ChangeState(component.UpgradeShopButton, PreviewState.BuyUpgrades).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.BackButton
                .OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(DelayClick))
                .Subscribe(_ => ChangeState(component.BackButton, PreviewState.Start).Forget())
                .AddTo(component.LifetimeDisposable);

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
                            SetActiveShopButtonsCanvasGroup(component, true);
                            component.BackButton.gameObject.SetActive(false);
                            component.BuyButton.gameObject.SetActive(false);
                            component.StartButton.gameObject.SetActive(true);
                            break;
                        case PreviewState.BuyWeapon:
                            SetActiveShopButtonsCanvasGroup(component, false);
                            component.BackButton.gameObject.SetActive(true);
                            component.BuyButton.gameObject.SetActive(true);
                            component.StartButton.gameObject.SetActive(false);
                            break;
                        case PreviewState.BuySkin:
                            SetActiveShopButtonsCanvasGroup(component, false);
                            component.BackButton.gameObject.SetActive(true);
                            component.BuyButton.gameObject.SetActive(true);
                            component.StartButton.gameObject.SetActive(false);
                            break;
                        case PreviewState.BuyUpgrades:
                            SetActiveShopButtonsCanvasGroup(component, false);
                            component.BackButton.gameObject.SetActive(true);
                            component.BuyButton.gameObject.SetActive(false);
                            component.StartButton.gameObject.SetActive(false);
                            break;
                    }
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SetActiveShopButtonsCanvasGroup(CShop component, bool isActive)
        {
            component.ShopButtonsCanvasGroup.alpha = isActive ? 1f : 0f;
            component.ShopButtonsCanvasGroup.interactable = isActive;
            component.ShopButtonsCanvasGroup.blocksRaycasts = isActive;
        }

        private async UniTaskVoid ChangeState(Button button, PreviewState state)
        {
            await button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();
            
            _characterPreviewModel.State.Value = state;
        }
    }
}
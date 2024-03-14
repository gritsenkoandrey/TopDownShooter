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
    public sealed class SShopElementsChangeState : SystemComponent<CShopElements>
    {
        private CharacterPreviewModel _characterPreviewModel;
        
        [Inject]
        private void Construct(CharacterPreviewModel characterPreviewModel)
        {
            _characterPreviewModel = characterPreviewModel;
        }
        
        protected override void OnEnableComponent(CShopElements component)
        {
            base.OnEnableComponent(component);
            
            component.WeaponShopButton
                .OnClickAsObservable()
                .ThrottleFirst(DelayClick())
                .Subscribe(_ => ChangeState(component.WeaponShopButton, PreviewState.BuyWeapon).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.SkinShopButton
                .OnClickAsObservable()
                .ThrottleFirst(DelayClick())
                .Subscribe(_ => ChangeState(component.SkinShopButton, PreviewState.BuySkin).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.UpgradeShopButton
                .OnClickAsObservable()
                .ThrottleFirst(DelayClick())
                .Subscribe(_ => ChangeState(component.UpgradeShopButton, PreviewState.BuyUpgrades).Forget())
                .AddTo(component.LifetimeDisposable);
            
            component.BackButton
                .OnClickAsObservable()
                .ThrottleFirst(DelayClick())
                .Subscribe(_ => ChangeState(component.BackButton, PreviewState.Start).Forget())
                .AddTo(component.LifetimeDisposable);
        }

        private async UniTaskVoid ChangeState(Button button, PreviewState state)
        {
            await button.transform.PunchTransform().AsyncWaitForCompletion().AsUniTask();
            
            _characterPreviewModel.State.Value = state;
        }

        private TimeSpan DelayClick() => TimeSpan.FromSeconds(ButtonSettings.DelayClick);
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using CodeBase.Utils;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewButtons : SystemComponent<CCharacterPreviewButtons>
    {
        private InventoryModel _inventoryModel;
        private CharacterPreviewModel _characterPreviewModel;

        [Inject]
        private void Construct(InventoryModel inventoryModel, CharacterPreviewModel characterPreviewModel)
        {
            _inventoryModel = inventoryModel;
            _characterPreviewModel = characterPreviewModel;
        }
        
        protected override void OnEnableComponent(CCharacterPreviewButtons component)
        {
            base.OnEnableComponent(component);

            SubscribeOnSelectCharacter(component);
            SubscribeOnClickButtons(component);
        }

        protected override void OnDisableComponent(CCharacterPreviewButtons component)
        {
            base.OnDisableComponent(component);
            
            component.ButtonDisposable.Clear();
        }

        private void SubscribeOnSelectCharacter(CCharacterPreviewButtons component)
        {
            component.SelectCharacter
                .Subscribe(_ =>
                {
                    _characterPreviewModel.CharacterPreview.CharacterPreviewAnimator.Animator.SetFloat(Animations.PreviewBlend, Random.Range(0, 4));
                    _characterPreviewModel.CharacterPreview.CharacterPreviewAnimator.Animator.SetTrigger(Animations.Preview);
                    
                    component.ButtonDisposable.Clear();
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnClickButtons(CCharacterPreviewButtons component)
        {
            component.UpButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.UpButton.transform.PunchTransform();
                    TurnUp(_characterPreviewModel.CharacterPreview.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
            
            component.DownButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.DownButton.transform.PunchTransform();
                    TurnDown(_characterPreviewModel.CharacterPreview.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
            
            component.LeftButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.LeftButton.transform.PunchTransform();
                    TurnLeft(_characterPreviewModel.CharacterPreview.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
            
            component.RightButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.RightButton.transform.PunchTransform();
                    TurnRight(_characterPreviewModel.CharacterPreview.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
        }

        private void TurnUp(CCharacterPreviewModel component)
        {
            int index = _inventoryModel.GetWeaponIndex();

            index--;

            if (index < 0)
            {
                index = component.Weapons.Length - 1;
            }
            
            _inventoryModel.SetWeaponIndex(index);
        }
        private void TurnDown(CCharacterPreviewModel component)
        {
            int index = _inventoryModel.GetWeaponIndex();

            index++;

            if (index > component.Weapons.Length - 1)
            {
                index = 0;
            }
            
            _inventoryModel.SetWeaponIndex(index);
        }
        private void TurnRight(CCharacterPreviewModel component)
        {
            int index = _inventoryModel.GetEquipmentIndex();

            index++;

            if (index > component.Heads.Length - 1)
            {
                index = 0;
            }
            
            _inventoryModel.SetEquipmentIndex(index);
        }
        private void TurnLeft(CCharacterPreviewModel component)
        {
            int index = _inventoryModel.GetEquipmentIndex();

            index--;

            if (index < 0)
            {
                index = component.Heads.Length - 1;
            }

            _inventoryModel.SetEquipmentIndex(index);
        }
    }
}
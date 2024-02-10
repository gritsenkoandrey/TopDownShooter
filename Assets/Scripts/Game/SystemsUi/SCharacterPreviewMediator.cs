using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.GUI;
using CodeBase.Infrastructure.Models;
using CodeBase.Infrastructure.Progress;
using CodeBase.Utils;
using UniRx;
using UnityEngine;
using VContainer;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewMediator : SystemComponent<CCharacterPreviewMediator>
    {
        private InventoryModel _inventoryModel;
        private IGuiService _guiService;
        private IProgressService _progressService;

        [Inject]
        private void Construct(InventoryModel inventoryModel, IGuiService guiService, IProgressService progressService)
        {
            _inventoryModel = inventoryModel;
            _guiService = guiService;
            _progressService = progressService;
        }
        
        protected override void OnEnableComponent(CCharacterPreviewMediator component)
        {
            base.OnEnableComponent(component);
            
            SubscribeOnChangeEquipment(component);
            SubscribeOnSelectCharacter(component);
            SubscribeOnClickButtons(component);
            SetOrthographicSizeOnPreviewCamera(component);
        }

        protected override void OnDisableComponent(CCharacterPreviewMediator component)
        {
            base.OnDisableComponent(component);
            
            component.ButtonDisposable.Clear();
        }

        private void SubscribeOnChangeEquipment(CCharacterPreviewMediator component)
        {
            _progressService.InventoryData.Data.Value
                .ObserveEveryValueChanged(data => data.WeaponIndex)
                .Subscribe(index =>
                {
                    SetWeapon(component.CharacterPreviewModel, index);
                    SetAnimatorController(component, index);
                })
                .AddTo(component.LifetimeDisposable);
            
            _progressService.InventoryData.Data.Value
                .ObserveEveryValueChanged(data => data.EquipmentIndex)
                .Subscribe(index =>
                {
                    SetEquipment(component.CharacterPreviewModel, index);
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnSelectCharacter(CCharacterPreviewMediator component)
        {
            component.SelectCharacter
                .Subscribe(_ =>
                {
                    component.CharacterPreviewAnimator.Animator.SetFloat(Animations.PreviewBlend, Random.Range(0, 4));
                    component.CharacterPreviewAnimator.Animator.SetTrigger(Animations.Preview);
                    
                    component.ButtonDisposable.Clear();
                })
                .AddTo(component.LifetimeDisposable);
        }

        private void SubscribeOnClickButtons(CCharacterPreviewMediator component)
        {
            component.CharacterPreviewButtons.UpButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.CharacterPreviewButtons.UpButton.transform.PunchTransform();
                    TurnUp(component.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
            
            component.CharacterPreviewButtons.DownButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.CharacterPreviewButtons.DownButton.transform.PunchTransform();
                    TurnDown(component.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
            
            component.CharacterPreviewButtons.LeftButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.CharacterPreviewButtons.LeftButton.transform.PunchTransform();
                    TurnLeft(component.CharacterPreviewModel);
                })
                .AddTo(component.ButtonDisposable);
            
            component.CharacterPreviewButtons.RightButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    component.CharacterPreviewButtons.RightButton.transform.PunchTransform();
                    TurnRight(component.CharacterPreviewModel);
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

        private void SetWeapon(CCharacterPreviewModel component, int index)
        {
            for (int i = 0; i < component.Weapons.Length; i++)
            {
                component.Weapons[i].Weapon.SetActive(i == index);
            }
            
            _inventoryModel.SetSelectedWeapon(component.Weapons[index].WeaponType);
        }

        private void SetEquipment(CCharacterPreviewModel component, int index)
        {
            for (int i = 0; i < component.Heads.Length; i++)
            {
                component.Heads[i].SetActive(i == index);
            }

            for (int i = 0; i < component.Bodies.Length; i++)
            {
                component.Bodies[i].SetActive(i == index);
            }
        }

        private void SetAnimatorController(CCharacterPreviewMediator component, int index)
        {
            component.CharacterPreviewAnimator.Animator.runtimeAnimatorController = 
                component.CharacterPreviewModel.Weapons[index].Weapon.RuntimeAnimatorController;
        }

        private void SetOrthographicSizeOnPreviewCamera(CCharacterPreviewMediator component)
        {
            component.PreviewCamera.orthographicSize *= _guiService.ScaleFactor;
        }
    }
}
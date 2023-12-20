using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using CodeBase.Infrastructure.Models;
using UniRx;

namespace CodeBase.Game.SystemsUi
{
    public sealed class SCharacterPreviewModel : SystemComponent<CCharacterPreviewModel>
    {
        private readonly InventoryModel _inventoryModel;

        public SCharacterPreviewModel(InventoryModel inventoryModel)
        {
            _inventoryModel = inventoryModel;
        }
        
        protected override void OnEnableComponent(CCharacterPreviewModel component)
        {
            base.OnEnableComponent(component);
            
            SetWeapon(component);
            SetCharacter(component);
            SetAnimatorController(component);

            component.PressUp
                .Subscribe(_ =>
                {
                    TurnUp(component);
                    SetAnimatorController(component);
                })
                .AddTo(component.LifetimeDisposable);
            
            component.PressDown
                .Subscribe(_ =>
                {
                    TurnDown(component);
                    SetAnimatorController(component);
                })
                .AddTo(component.LifetimeDisposable);
            
            component.PressLeft
                .Subscribe(_ => TurnLeft(component))
                .AddTo(component.LifetimeDisposable);
            
            component.PressRight
                .Subscribe(_ => TurnRight(component))
                .AddTo(component.LifetimeDisposable);
        }

        private void TurnUp(CCharacterPreviewModel component)
        {
            component.IndexWeapon--;

            if (component.IndexWeapon < 0)
            {
                component.IndexWeapon = component.Weapons.Length - 1;
            }
            
            SetWeapon(component);
        }

        private void TurnDown(CCharacterPreviewModel component)
        {
            component.IndexWeapon++;
            
            if (component.IndexWeapon > component.Weapons.Length - 1)
            {
                component.IndexWeapon = 0;
            }

            SetWeapon(component);
        }

        private void TurnRight(CCharacterPreviewModel component)
        {
            component.IndexCharacter++;

            if (component.IndexCharacter > component.Heads.Length - 1)
            {
                component.IndexCharacter = 0;
            }
            
            SetCharacter(component);
        }

        private void TurnLeft(CCharacterPreviewModel component)
        {
            component.IndexCharacter--;

            if (component.IndexCharacter < 0)
            {
                component.IndexCharacter = component.Heads.Length - 1;
            }
            
            SetCharacter(component);
        }

        private void SetWeapon(CCharacterPreviewModel component)
        {
            for (int i = 0; i < component.Weapons.Length; i++)
            {
                component.Weapons[i].Weapon.SetActive(i == component.IndexWeapon);
            }
            
            _inventoryModel.SelectedWeapon = component.Weapons[component.IndexWeapon].WeaponType;
        }

        private void SetCharacter(CCharacterPreviewModel component)
        {
            for (int i = 0; i < component.Heads.Length; i++)
            {
                component.Heads[i].SetActive(i == component.IndexCharacter);
            }

            for (int i = 0; i < component.Bodies.Length; i++)
            {
                component.Bodies[i].SetActive(i == component.IndexCharacter);
            }

            _inventoryModel.BodyIndex = component.IndexCharacter;
        }

        private void SetAnimatorController(CCharacterPreviewModel component)
        {
            component.CharacterPreviewAnimator.Animator.runtimeAnimatorController = 
                component.Weapons[component.IndexWeapon].Weapon.AnimatorController;
        }
    }
}
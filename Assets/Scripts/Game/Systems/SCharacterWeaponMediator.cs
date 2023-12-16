using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon;

namespace CodeBase.Game.Systems
{
    public sealed class SCharacterWeaponMediator : SystemComponent<CCharacter>
    {
        protected override void OnEnableComponent(CCharacter component)
        {
            base.OnEnableComponent(component);
            
            component.WeaponMediator.SetWeapon(WeaponType.Rifle);
        }
    }
}
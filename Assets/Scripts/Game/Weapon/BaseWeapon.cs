using CodeBase.Game.Weapon.Factories;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseWeapon
    {
        protected BaseWeapon(IWeaponFactory weaponFactory) { }
    }
}
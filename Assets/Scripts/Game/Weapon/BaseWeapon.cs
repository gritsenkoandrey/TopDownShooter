using CodeBase.Game.Weapon.Factories;
using CodeBase.Infrastructure.Progress;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseWeapon
    {
        protected BaseWeapon(IWeaponFactory weaponFactory,  IProgressService progressService, WeaponType weaponType) { }
    }
}
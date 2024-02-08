using CodeBase.Game.Components;
using CodeBase.Infrastructure.StaticData.Data;

namespace CodeBase.Game.Weapon
{
    public abstract class BaseWeapon
    {
        private protected CWeapon Weapon;
        private protected WeaponCharacteristic WeaponCharacteristic;

        protected BaseWeapon(CWeapon weapon, WeaponCharacteristic weaponCharacteristic)
        {
        }

        public virtual void Initialize()
        {
        }
    }
}
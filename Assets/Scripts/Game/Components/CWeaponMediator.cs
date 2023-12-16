using System.Collections.Generic;
using System.Linq;
using CodeBase.ECSCore;
using CodeBase.Game.Weapon;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeaponMediator : EntityComponent<CWeaponMediator>
    {
        [SerializeField] private CWeapon[] _weapons;

        private IReadOnlyDictionary<WeaponType, CWeapon> _weaponsDictionary;
        public CWeapon CurrentWeapon { get; private set; }
        public void SetWeapon(WeaponType weaponType)
        {
            SetActiveCurrentWeapon(false);

            CurrentWeapon = _weaponsDictionary.TryGetValue(weaponType, out CWeapon weapon) ? weapon : null;
            
            SetActiveCurrentWeapon(true);
        }

        protected override void OnEntityCreate()
        {
            base.OnEntityCreate();

            _weaponsDictionary = _weapons.ToDictionary(weapon => weapon.WeaponType, weapon => weapon);
        }

        private void SetActiveCurrentWeapon(bool value)
        {
            if (CurrentWeapon != null)
            {
                CurrentWeapon.SetActive(value);
            }
        }
    }
}
using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CWeaponMediator : EntityComponent<CWeaponMediator>
    {
        [SerializeField] private Transform _container;
        public Transform Container => _container;
        public CWeapon CurrentWeapon { get; private set; }

        public void SetWeapon(CWeapon weapon)
        {
            if (CurrentWeapon != null)
            {
                Destroy(CurrentWeapon.gameObject);
            }

            CurrentWeapon = weapon;
        }
    }
}
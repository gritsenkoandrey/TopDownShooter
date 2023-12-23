using System;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewModel : EntityComponent<CCharacterPreviewModel>
    {
        [SerializeField] private GameObject[] _heads;
        [SerializeField] private GameObject[] _bodies;
        [SerializeField] private WeaponData[] _weapons;

        public GameObject[] Heads => _heads;
        public GameObject[] Bodies => _bodies;
        public WeaponData[] Weapons => _weapons;
    }

    [Serializable]
    public struct WeaponData
    {
        public WeaponType WeaponType;
        public CWeapon Weapon;
    }
}
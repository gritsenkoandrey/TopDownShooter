using System;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewModel : EntityComponent<CCharacterPreviewModel>
    {
        [SerializeField] private WeaponData[] _weapons;
        [SerializeField] private SkinData[] _skins;

        public SkinData[] Skins => _skins;
        public WeaponData[] Weapons => _weapons;
    }

    [Serializable]
    public struct WeaponData
    {
        public WeaponType WeaponType;
        public CWeapon Weapon;
    }
}
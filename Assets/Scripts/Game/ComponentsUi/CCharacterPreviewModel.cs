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
        [SerializeField] private Material[] _equpmentMaterials;
        [SerializeField] private Material[] _weaponMaterials;

        public SkinData[] Skins => _skins;
        public WeaponData[] Weapons => _weapons;
        public Material[] EquipmentMaterials => _equpmentMaterials;
        public Material[] WeaponMaterials => _weaponMaterials;
    }

    [Serializable]
    public struct WeaponData
    {
        public WeaponType WeaponType;
        public CWeapon Weapon;
    }
    
    [Serializable]
    public struct SkinData
    {
        public SkinType Type;
        public VisualData Data;
    }

    [Serializable]
    public struct VisualData
    {
        public GameObject[] Visual;
    }
}
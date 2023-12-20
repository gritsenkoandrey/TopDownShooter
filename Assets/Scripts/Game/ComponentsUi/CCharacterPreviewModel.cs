using System;
using CodeBase.ECSCore;
using CodeBase.Game.Components;
using CodeBase.Game.Weapon;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterPreviewModel : EntityComponent<CCharacterPreviewModel>
    {
        [SerializeField] private CCharacterPreviewAnimator _characterPreviewAnimator;
        [SerializeField] private GameObject[] _heads;
        [SerializeField] private GameObject[] _bodies;
        [SerializeField] private WeaponData[] _weapons;

        public CCharacterPreviewAnimator CharacterPreviewAnimator => _characterPreviewAnimator;
        public GameObject[] Heads => _heads;
        public GameObject[] Bodies => _bodies;
        public WeaponData[] Weapons => _weapons;

        public readonly ReactiveCommand PressUp = new();
        public readonly ReactiveCommand PressDown = new();
        public readonly ReactiveCommand PressLeft = new();
        public readonly ReactiveCommand PressRight = new();

        public int IndexCharacter = 0;
        public int IndexWeapon = 0;
    }

    [Serializable]
    public struct WeaponData
    {
        public WeaponType WeaponType;
        public CWeapon Weapon;
    }
}
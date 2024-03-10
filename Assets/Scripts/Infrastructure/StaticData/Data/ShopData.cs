using System;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ShopData), menuName = "Data/" + nameof(ShopData))]
    public sealed class ShopData : ScriptableObject
    {
        public WeaponShopData[] WeaponsShopData;
        public SkinShopData[] SkinsShopData;
    }

    [Serializable]
    public struct WeaponShopData
    {
        public WeaponType WeaponType;
        public int Cost;
    }

    [Serializable]
    public struct SkinShopData
    {
        public SkinType SkinType;
        public int Cost;
    }
}
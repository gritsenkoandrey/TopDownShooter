using System;
using System.Collections.Generic;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.SaveLoad;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class ShopData : ISaveLoad<Shop>
    {
        public IReactiveProperty<Shop> Data { get; }

        public ShopData()
        {
            Data = new ReactiveProperty<Shop>(Load());
            Data.Value.Save += Save;
        }

        public void Save(Shop data)
        {
            PlayerPrefs.SetString(DataKeys.Shop, data.ToSerialize());
            PlayerPrefs.Save();
        }

        public Shop Load()
        {
            return PlayerPrefs.HasKey(DataKeys.Shop)
                ? PlayerPrefs.GetString(DataKeys.Shop)?.ToDeserialize<Shop>()
                : SetDefaultValue();
        }

        private Shop SetDefaultValue()
        {
            List<WeaponType> weapons = new List<WeaponType>
            {
                WeaponType.Knife
            };

            List<SkinType> skins = new List<SkinType>
            {
                SkinType.BasicMale,
                SkinType.BasicFemale,
            };
            
            return new Shop(weapons, skins);
        }

        void IDisposable.Dispose() => Data.Value.Save -= Save;
    }

    [JsonObject]
    public sealed class Shop
    {
        [JsonProperty] private readonly List<WeaponType> _weapons;
        [JsonProperty] private readonly List<SkinType> _skins;

        [JsonIgnore] public IReadOnlyList<WeaponType> Weapons => _weapons;
        [JsonIgnore] public IReadOnlyList<SkinType> Skins => _skins;

        public event Action<Shop> Save;

        public Shop(List<WeaponType> weapons, List<SkinType> skins)
        {
            _weapons = weapons;
            _skins = skins;
        }

        public void Add(WeaponType type)
        {
            _weapons.Add(type);
            
            Save?.Invoke(this);
        }

        public void Add(SkinType type)
        {
            _skins.Add(type);
            
            Save?.Invoke(this);
        }

        public bool Contains(WeaponType type) => _weapons.Contains(type);
        
        public bool Contains(SkinType type) => _skins.Contains(type);
    }
}
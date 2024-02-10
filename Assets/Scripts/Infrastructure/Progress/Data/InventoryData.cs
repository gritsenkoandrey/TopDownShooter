using System;
using CodeBase.Infrastructure.SaveLoad;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class InventoryData : ISaveLoad<Inventory>
    {
        private readonly CompositeDisposable _disposable;

        public IReactiveProperty<Inventory> Data { get; }

        public InventoryData()
        {
            Data = new ReactiveProperty<Inventory>(Load());
            
            _disposable = new CompositeDisposable();
            
            SubscribeOnDataChanged();
        }

        public void Save(Inventory data)
        {
            PlayerPrefs.SetString(DataKeys.Inventory, data.ToSerialize());
            PlayerPrefs.Save();
        }

        public Inventory Load()
        {
            return PlayerPrefs.HasKey(DataKeys.Inventory)
                ? PlayerPrefs.GetString(DataKeys.Inventory)?.ToDeserialize<Inventory>()
                : SetDefaultValue();
        }

        private Inventory SetDefaultValue()
        {
            Inventory inventory = new Inventory
            {
                WeaponIndex = default,
                EquipmentIndex = default
            };

            return inventory;
        }

        private void SubscribeOnDataChanged()
        {
            Data.Value
                .ObserveEveryValueChanged(data => data.WeaponIndex)
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
            
            Data.Value
                .ObserveEveryValueChanged(data => data.EquipmentIndex)
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
        }

        void IDisposable.Dispose() => _disposable.Clear();
    }

    public sealed class Inventory
    {
        public int WeaponIndex;
        public int EquipmentIndex;
    }
}
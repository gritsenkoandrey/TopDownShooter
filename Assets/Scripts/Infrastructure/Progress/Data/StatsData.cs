using System.Collections.Generic;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class StatsData : ISaveLoad<IDictionary<UpgradeButtonType, int>>
    {
        private readonly CompositeDisposable _disposable;
        
        private const int DefaultValue = 1;

        public IReactiveProperty<IDictionary<UpgradeButtonType, int>> Data { get; }

        public StatsData()
        {
            Data = new ReactiveProperty<IDictionary<UpgradeButtonType, int>>(Load());
            
            _disposable = new CompositeDisposable();
            
            Data.Value.Keys.Foreach(SubscribeOnDataChanged);
        }
        
        public void Save(IDictionary<UpgradeButtonType, int> data)
        {
            PlayerPrefs.SetString(DataKeys.Stats, data.ToSerialize());
            PlayerPrefs.Save();
        }

        public IDictionary<UpgradeButtonType, int> Load() => PlayerPrefs.HasKey(DataKeys.Stats)
            ? PlayerPrefs.GetString(DataKeys.Stats)?.ToDeserialize<Dictionary<UpgradeButtonType, int>>()
            : SetDefaultValue();

        public void Dispose() => _disposable.Clear();

        private void SubscribeOnDataChanged(UpgradeButtonType type)
        {
            Data.Value
                .ObserveEveryValueChanged(data => data[type])
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
        }

        private Dictionary<UpgradeButtonType, int> SetDefaultValue()
        {
            return new Dictionary<UpgradeButtonType, int>
            {
                { UpgradeButtonType.Damage, DefaultValue},
                { UpgradeButtonType.Health, DefaultValue},
                { UpgradeButtonType.Speed, DefaultValue}
            };
        }
    }

    public sealed class Stats
    {
        public IDictionary<UpgradeButtonType, int> Data;
    }
}
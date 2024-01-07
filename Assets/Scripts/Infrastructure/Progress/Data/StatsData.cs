using System.Collections.Generic;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.SaveLoad;
using CodeBase.Utils;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class StatsData : ISaveLoad<Stats>
    {
        private readonly CompositeDisposable _disposable;
        
        private const int DefaultValue = 1;

        public IReactiveProperty<Stats> Data { get; }

        public StatsData()
        {
            Data = new ReactiveProperty<Stats>(Load());
            
            _disposable = new CompositeDisposable();
            
            Data.Value.Data.Keys.Foreach(SubscribeOnDataChanged);
        }
        
        public void Save(Stats data)
        {
            PlayerPrefs.SetString(DataKeys.Stats, data.ToSerialize());
            PlayerPrefs.Save();
        }

        public Stats Load() => PlayerPrefs.HasKey(DataKeys.Stats)
            ? PlayerPrefs.GetString(DataKeys.Stats)?.ToDeserialize<Stats>()
            : SetDefaultValue();

        public void Dispose() => _disposable.Clear();

        private void SubscribeOnDataChanged(UpgradeButtonType type)
        {
            Data.Value
                .ObserveEveryValueChanged(data => data.Data[type])
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
        }

        private Stats SetDefaultValue()
        {
            Stats stats = new Stats
            {
                Data = new Dictionary<UpgradeButtonType, int>
                {
                    { UpgradeButtonType.Damage, DefaultValue },
                    { UpgradeButtonType.Health, DefaultValue },
                    { UpgradeButtonType.Speed, DefaultValue }
                }
            };

            return stats;
        }
    }

    public sealed class Stats
    {
        public IDictionary<UpgradeButtonType, int> Data;
    }
}
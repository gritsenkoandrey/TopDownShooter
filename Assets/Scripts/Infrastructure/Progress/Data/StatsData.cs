using System;
using CodeBase.Infrastructure.SaveLoad;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class StatsData : ISaveLoad<Stats>
    {
        private readonly CompositeDisposable _disposable;

        public IReactiveProperty<Stats> Data { get; }

        public StatsData()
        {
            Data = new ReactiveProperty<Stats>(Load());
            
            _disposable = new CompositeDisposable();

            Data.Value
                .ObserveEveryValueChanged(stats => stats.Health)
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
            
            Data.Value
                .ObserveEveryValueChanged(stats => stats.Damage)
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
            
            Data.Value
                .ObserveEveryValueChanged(stats => stats.Speed)
                .Subscribe(_ => Save(Data.Value))
                .AddTo(_disposable);
        }
        
        public void Save(Stats data)
        {
            PlayerPrefs.SetString(DataKeys.Stats, data.ToSerialize());
            PlayerPrefs.Save();
        }

        public Stats Load() => PlayerPrefs.HasKey(DataKeys.Stats)
                ? PlayerPrefs.GetString(DataKeys.Stats)?.ToDeserialize<Stats>()
                : new Stats();

        public void Dispose() => _disposable.Clear();
    }
    
    [Serializable]
    public sealed class Stats
    {
        public int Damage;
        public int Health;
        public int Speed;

        public Stats()
        {
            Damage = 1;
            Health = 1;
            Speed = 1;
        }
    }
}
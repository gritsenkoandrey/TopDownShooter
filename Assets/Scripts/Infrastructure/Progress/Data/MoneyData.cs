using System;
using CodeBase.Infrastructure.SaveLoad;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class MoneyData : ISaveLoad<int>
    {
        private readonly IDisposable _disposable;

        public IReactiveProperty<int> Data { get; }

        public MoneyData()
        {
            Data = new ReactiveProperty<int>(Load());

            _disposable = Data.Subscribe(Save);
        }

        public void Save(int data)
        {
            PlayerPrefs.SetInt(DataKeys.Money, data);
            PlayerPrefs.Save();
        }

        public int Load() => PlayerPrefs.GetInt(DataKeys.Money, default);

        void IDisposable.Dispose() => _disposable?.Dispose();
    }
}
using System;
using CodeBase.Infrastructure.SaveLoad;
using UniRx;
using UnityEngine;

namespace CodeBase.Infrastructure.Progress.Data
{
    public sealed class HapticData : ISaveLoad<bool>
    {
        private readonly IDisposable _disposable;
        
        private const int DefaultValue = 1;
        
        public IReactiveProperty<bool> Data { get; }

        public HapticData()
        {
            Data = new ReactiveProperty<bool>(Load());

            _disposable = Data.Subscribe(Save);
        }

        public void Save(bool data)
        {
            PlayerPrefs.SetInt(DataKeys.Haptic, data == false ? 0 : 1);
            PlayerPrefs.Save();
        }

        public bool Load()
        {
            int data = PlayerPrefs.GetInt(DataKeys.Haptic, DefaultValue);
            
            return data != 0;
        }

        void IDisposable.Dispose() => _disposable?.Dispose();
    }
}
using UniRx;
using UnityEngine;

namespace CodeBase.ECSCore
{
    public abstract class Entity : MonoBehaviour
    {
        public readonly CompositeDisposable LifetimeDisposable = new();
        
        public bool IsEnabled { get; private set; }

        protected virtual void OnEntityCreate() { }
        protected virtual void OnEntityEnable() => IsEnabled = true;
        protected virtual void OnEntityDisable() => IsEnabled = false;
    }
}
using UniRx;
using UnityEngine;

namespace CodeBase.ECSCore
{
    public abstract class Entity : MonoBehaviour
    {
        public readonly CompositeDisposable LifetimeDisposable = new();

        protected virtual void OnEntityCreate() { }
        protected virtual void OnEntityEnable() { }
        protected virtual void OnEntityDisable() { }
    }
}
using System;
using UniRx;
using UnityEngine;

namespace AndreyGritsenko.ECSCore
{
    public abstract class Entity : MonoBehaviour
    {
        public readonly CompositeDisposable LifetimeDisposable = new();
        
        public static Action<Entity> OnRegistered;
        public static Action<Entity> OnUnregistered;

        protected virtual void OnEntityCreate() { }
        protected virtual void OnEntityEnable() { }
        protected virtual void OnEntityDisable() { }
    }
}

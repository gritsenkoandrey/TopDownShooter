﻿using System.Collections.Generic;
using UniRx;

namespace AndreyGritsenko.ECSCore
{
    public abstract class System
    {
        protected readonly CompositeDisposable LifetimeDisposable;
        protected readonly HashSet<Entity> Entities;

        protected System()
        {
            LifetimeDisposable = new CompositeDisposable();
            Entities = new HashSet<Entity>();
        }
        
        public void EnableSystem() => OnEnableSystem();
        public void DisableSystem() => OnDisableSystem();

        protected virtual void OnEnableSystem() { }
        protected virtual void OnDisableSystem()
        {
            LifetimeDisposable.Clear();
            Entities.Clear();
        }
    }
}
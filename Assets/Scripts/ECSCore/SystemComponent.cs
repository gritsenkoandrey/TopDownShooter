﻿using System.Collections.Generic;

namespace CodeBase.ECSCore
{
    public abstract class SystemComponent<T> : SystemBase where T : Entity
    {
        private readonly HashSet<T> _entities;

        protected IReadOnlyCollection<T> Entities => _entities;

        protected SystemComponent()
        {
            _entities = new HashSet<T>();
        }

        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            EntityContainer<T>.OnRegistered += OnEnableComponent;
            EntityContainer<T>.OnUnregistered += OnDisableComponent;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            EntityContainer<T>.OnRegistered -= OnEnableComponent;
            EntityContainer<T>.OnUnregistered -= OnDisableComponent;
        }

        public override void Dispose()
        {
            base.Dispose();
            
            _entities.Clear();
        }

        protected virtual void OnEnableComponent(T component) => _entities.Add(component);
        protected virtual void OnDisableComponent(T component) => _entities.Remove(component);
    }
}
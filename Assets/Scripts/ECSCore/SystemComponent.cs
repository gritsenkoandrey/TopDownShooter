using System.Collections.Generic;

namespace CodeBase.ECSCore
{
    public abstract class SystemComponent<T> : SystemBase where T : Entity
    {
        protected readonly HashSet<T> Entities;
        
        protected SystemComponent()
        {
            Entities = new HashSet<T>();
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
            
            Entities.Clear();
        }

        protected virtual void OnEnableComponent(T component) => Entities.Add(component);
        protected virtual void OnDisableComponent(T component) => Entities.Remove(component);
    }
}
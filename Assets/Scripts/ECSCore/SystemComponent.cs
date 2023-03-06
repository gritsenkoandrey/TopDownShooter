namespace AndreyGritsenko.ECSCore
{
    public abstract class SystemComponent<T> : System where T : Entity
    {
        protected override void OnEnableSystem()
        {
            base.OnEnableSystem();
            
            Entity.OnRegistered += Register;
            Entity.OnUnregistered += Unregister;
        }

        protected override void OnDisableSystem()
        {
            base.OnDisableSystem();
            
            Entity.OnRegistered -= Register;
            Entity.OnUnregistered -= Unregister;
        }

        protected virtual void OnEnableComponent(T component) => Entities.Add(component);
        protected virtual void OnDisableComponent(T component) => Entities.Remove(component);

        private void Register(Entity entity)
        {
            if (entity as T)
            {
                OnEnableComponent((T)entity);
            }
        }

        private void Unregister(Entity entity)
        {
            if (entity as T)
            {
                OnDisableComponent((T)entity);
            }
        }
    }
}
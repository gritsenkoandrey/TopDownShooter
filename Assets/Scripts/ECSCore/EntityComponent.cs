namespace CodeBase.ECSCore
{
    public abstract class EntityComponent<T> : Entity where T : Entity
    {
        private void Awake()
        {
            OnEntityCreate();
        }

        private void OnEnable()
        {
            OnEntityEnable();
            
            EntityContainer<T>.Registered(this);
        }

        private void OnDisable()
        {
            OnEntityDisable();
            
            LifetimeDisposable.Clear();

            EntityContainer<T>.Unregistered(this);
        }
    }
}
namespace AndreyGritsenko.ECSCore
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
            
            EntityContainer<T>.OnRegistered?.Invoke(this as T);
        }

        private void OnDisable()
        {
            OnEntityDisable();
            
            LifetimeDisposable.Clear();

            EntityContainer<T>.OnUnregistered?.Invoke(this as T);
        }
    }
}
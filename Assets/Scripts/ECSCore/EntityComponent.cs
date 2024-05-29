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

            EntityContainer<T>.Unregistered(this);
        }

        private void OnDestroy()
        {
            OnEntityDestroy();
        }
    }
}
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
            
            OnRegistered?.Invoke(this);
        }

        private void OnDisable()
        {
            OnEntityDisable();
            
            OnUnregistered?.Invoke(this);
        }
    }
}
namespace CodeBase.ECSCore
{
    public static class EntityExtension
    {
        public static void SetActive(this Entity entity, bool value) => entity.gameObject.SetActive(value);

        public static void CleanSubscribe(this Entity entity) => entity.LifetimeDisposable.Clear();
    }
}
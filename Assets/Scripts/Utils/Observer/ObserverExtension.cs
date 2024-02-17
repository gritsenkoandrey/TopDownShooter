namespace CodeBase.Utils.Observer
{
    public static class ObserverExtension
    {
        public static void Execute<T>(this Observer<T> observer, T value)
        {
            observer.Value = value;
        }
    }
}
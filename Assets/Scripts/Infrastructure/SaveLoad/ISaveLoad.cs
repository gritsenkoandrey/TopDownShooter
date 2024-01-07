using CodeBase.Infrastructure.Progress;

namespace CodeBase.Infrastructure.SaveLoad
{
    public interface ISaveLoad<T> : IData<T>
    {
        public void Save(T data);
        public T Load();
    }
}
using CodeBase.Infrastructure.Progress;

namespace CodeBase.Infrastructure.SaveLoad
{
    public interface ISaveLoad<T> : IData<T>
    {
        void Save(T data);
        T Load();
    }
}
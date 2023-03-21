using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.AssetData
{
    public interface IAsset : IService
    {
        public T Load<T>(string path) where T : class;
    }
}
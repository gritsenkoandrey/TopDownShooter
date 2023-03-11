using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.Data
{
    public interface IAsset : IService
    {
        public GameAssetData GameAssetData { get; }
        public UiAssetData UiAssetData { get; }
    }
}
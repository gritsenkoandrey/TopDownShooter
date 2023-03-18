using CodeBase.Infrastructure.AssetData.Data;
using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.AssetData
{
    public interface IAsset : IService
    {
        public GameAssetData GameAssetData { get; }
        public UiAssetData UiAssetData { get; }
    }
}
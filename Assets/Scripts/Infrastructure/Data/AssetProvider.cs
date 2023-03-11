using UnityEngine;

namespace CodeBase.Infrastructure.Data
{
    public sealed class AssetProvider : IAsset
    {
        private const string GameAssetDataPath = "Data/GameAssetData";
        private const string UiAssetDataPath = "Data/UiAssetData";
        
        public AssetProvider()
        {
            GameAssetData = Load<GameAssetData>(GameAssetDataPath);
            UiAssetData = Load<UiAssetData>(UiAssetDataPath);
        }

        public GameAssetData GameAssetData { get; }
        public UiAssetData UiAssetData { get; }

        private T Load<T>(string path) where T : ScriptableObject => Resources.Load<T>(path);
    }
}
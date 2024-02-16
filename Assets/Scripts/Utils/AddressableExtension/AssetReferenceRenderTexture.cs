using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Utils.AddressableExtension
{
    [System.Serializable]
    public sealed class AssetReferenceRenderTexture : AssetReferenceT<RenderTexture>
    {
        public AssetReferenceRenderTexture(string guid) : base(guid)
        {
        }
    }
}
using CodeBase.UI.Screens;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ScreenData), menuName = "Data/" + nameof(ScreenData))]
    public sealed class ScreenData : ScriptableObject
    {
        public ScreenType ScreenType;
        public AssetReference PrefabReference;
    }
}
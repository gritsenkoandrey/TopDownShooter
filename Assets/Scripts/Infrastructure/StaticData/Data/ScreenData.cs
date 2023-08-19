using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(ScreenData), menuName = "Data/" + nameof(ScreenData))]
    public sealed class ScreenData : ScriptableObject
    {
        public ScreenType ScreenType;
        public BaseScreen Prefab;
    }
}
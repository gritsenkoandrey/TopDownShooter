using CodeBase.UI.Screens;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "ScreenData", menuName = "Data/ScreenData", order = 0)]
    public sealed class ScreenData : ScriptableObject
    {
        public ScreenType ScreenType;
        public BaseScreen Prefab;
    }
}
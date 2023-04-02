using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "FxData", menuName = "Data/FxData", order = 0)]
    public sealed class FxData : ScriptableObject
    {
        public GameObject HitFx;
        public GameObject DeatFx;
    }
}
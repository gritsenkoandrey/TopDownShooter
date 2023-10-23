using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(FxData), menuName = "Data/" + nameof(FxData))]
    public sealed class FxData : ScriptableObject
    {
        public float HitFxReleaseTime;
        public float DeathFxReleaseTime;
    }
}
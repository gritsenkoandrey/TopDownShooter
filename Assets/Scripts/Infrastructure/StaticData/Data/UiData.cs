using CodeBase.Game.ComponentsUi;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = "UiData", menuName = "Data/UiData", order = 0)]
    public sealed class UiData : ScriptableObject
    {
        public CEnemyHealth EnemyHealth;
    }
}
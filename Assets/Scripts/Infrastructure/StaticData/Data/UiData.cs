using CodeBase.Game.ComponentsUi;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData.Data
{
    [CreateAssetMenu(fileName = nameof(UiData), menuName = "Data/" + nameof(UiData))]
    public sealed class UiData : ScriptableObject
    {
        public CEnemyHealth EnemyHealth;
    }
}
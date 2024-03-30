using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CPrintResultText : EntityComponent<CPrintResultText>
    {
        [SerializeField] private Transform[] _letters;

        public Transform[] Letters => _letters;
    }
}
using CodeBase.ECSCore;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CPrintResultText : EntityComponent<CPrintResultText>
    {
        [SerializeField] private Letter[] _letters;

        public Letter[] Letters => _letters;
    }

    [System.Serializable]
    public sealed class Letter
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Transform _transform;
        
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Transform Transform => _transform;
        public Sequence Sequence { get; set; }
    }
}
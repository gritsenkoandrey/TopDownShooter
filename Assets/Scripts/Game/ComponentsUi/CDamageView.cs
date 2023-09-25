using CodeBase.ECSCore;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CDamageView : EntityComponent<CDamageView>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;

        public TextMeshProUGUI Text => _text;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public Sequence Sequence { get; set; }
    }
}
using CodeBase.ECSCore;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CBloodEffect : EntityComponent<CBloodEffect>
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup => _canvasGroup;
        public Tween BloodTween { get; set; }
    }
}
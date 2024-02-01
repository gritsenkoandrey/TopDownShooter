using CodeBase.ECSCore;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CWeaponReloadingView : EntityComponent<CWeaponReloadingView>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _fill;
        [SerializeField] private Transform _transform;

        public CanvasGroup CanvasGroup => _canvasGroup;
        public Image Fill => _fill;
        public Transform Transform => _transform;
        public Tween Tween { get; set; }
    }
}
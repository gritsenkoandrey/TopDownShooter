using CodeBase.ECSCore;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterHealth : EntityComponent<CCharacterHealth>
    {
        [SerializeField] private Image _fill;
        [SerializeField] private Image _fillLerp;
        [SerializeField] private TextMeshProUGUI _text;
        
        public Image Fill => _fill;
        public Image FillLerp => _fillLerp;
        public TextMeshProUGUI Text => _text;
        public Tween Tween { get; set; }
    }
}
using CodeBase.ECSCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterHealth : EntityComponent<CCharacterHealth>
    {
        [SerializeField] private Image _fill;
        [SerializeField] private TextMeshProUGUI _text;
        
        public Image Fill => _fill;
        public TextMeshProUGUI Text => _text;
    }
}
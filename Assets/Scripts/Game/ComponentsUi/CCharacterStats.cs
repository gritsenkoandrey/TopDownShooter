using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCharacterStats : EntityComponent<CCharacterStats>
    {
        [SerializeField] private TextMeshProUGUI _textHealth;
        [SerializeField] private TextMeshProUGUI _textDamage;

        public TextMeshProUGUI TextHealth => _textHealth;
        public TextMeshProUGUI TextDamage => _textDamage;
    }
}
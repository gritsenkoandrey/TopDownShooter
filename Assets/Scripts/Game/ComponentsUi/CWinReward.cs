using CodeBase.ECSCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CWinReward : EntityComponent<CWinReward>
    {
        [SerializeField] private Image[] _stars;
        [SerializeField] private TextMeshProUGUI _text;

        public Image[] Stars => _stars;
        public TextMeshProUGUI Text => _text;
    }
}
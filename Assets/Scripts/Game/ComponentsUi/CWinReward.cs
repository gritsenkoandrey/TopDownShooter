using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CWinReward : EntityComponent<CWinReward>
    {
        [SerializeField] private GameObject[] _stars;
        [SerializeField] private GameObject[] _elements;
        [SerializeField] private TextMeshProUGUI _text;

        public GameObject[] Stars => _stars;
        public GameObject[] Elements => _elements;
        public TextMeshProUGUI Text => _text;
    }
}
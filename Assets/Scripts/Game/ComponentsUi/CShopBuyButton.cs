using CodeBase.ECSCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShopBuyButton : EntityComponent<CShopBuyButton>
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _text;

        public Button Button => _button;
        public TextMeshProUGUI Text => _text;
    }
}
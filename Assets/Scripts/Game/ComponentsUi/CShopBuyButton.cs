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
        [SerializeField] private Color _buyColor;
        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _selectedColor;

        public Button Button => _button;
        public TextMeshProUGUI Text => _text;
        public Color BuyColor => _buyColor;
        public Color SelectColor => _selectColor;
        public Color SelectedColor => _selectedColor;
    }
}
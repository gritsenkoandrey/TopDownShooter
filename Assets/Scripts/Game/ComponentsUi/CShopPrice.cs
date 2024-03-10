using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CShopPrice : EntityComponent<CShopPrice>
    {
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private CanvasGroup _canvasGroup;

        public TextMeshProUGUI CostText => _costText;
        public CanvasGroup CanvasGroup => _canvasGroup;
    }
}
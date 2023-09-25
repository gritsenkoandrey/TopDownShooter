using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CMoneyUpdate : EntityComponent<CMoneyUpdate>
    {
        [SerializeField] private TextMeshProUGUI _textCountMoney;

        public TextMeshProUGUI TextCountMoney => _textCountMoney;
    }
}
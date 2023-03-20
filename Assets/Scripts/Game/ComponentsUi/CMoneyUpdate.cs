using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CMoneyUpdate : EntityComponent<CMoneyUpdate>
    {
        [SerializeField] private TextMeshProUGUI _textCountMoney;

        public TextMeshProUGUI TextCountMoney => _textCountMoney;
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
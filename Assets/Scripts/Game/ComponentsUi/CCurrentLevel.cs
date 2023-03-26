using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CCurrentLevel : EntityComponent<CCurrentLevel>
    {
        [SerializeField] private TextMeshProUGUI _textLevel;

        public TextMeshProUGUI TextLevel => _textLevel;
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
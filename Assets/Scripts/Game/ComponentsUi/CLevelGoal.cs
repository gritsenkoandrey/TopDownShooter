using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CLevelGoal : EntityComponent<CLevelGoal>
    {
        [SerializeField] private TextMeshProUGUI _textLevelGoal;
        [SerializeField] private GameObject _background;

        public TextMeshProUGUI TextLevelGoal => _textLevelGoal;
        public GameObject Background => _background;
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
using CodeBase.ECSCore;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CSettingsButton : EntityComponent<CSettingsButton>
    {
        [SerializeField] private Button _button;

        public Button Button => _button;
        public Tween Tween { get; set; }
    }
}
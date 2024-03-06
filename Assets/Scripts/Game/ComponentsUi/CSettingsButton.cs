using CodeBase.ECSCore;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CSettingsButton : EntityComponent<CSettingsButton>
    {
        [SerializeField] private Button _button;
        [SerializeField] private Transform _image;

        public Button Button => _button;
        public Transform Image => _image;
        
        public Tween Tween { get; set; }
    }
}
using CodeBase.ECSCore;
using TMPro;
using UniRx;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CLevelTimeLeft : EntityComponent<CLevelTimeLeft>
    {
        [SerializeField] private TextMeshProUGUI _timeLeftText;

        public TextMeshProUGUI TimeLeftText => _timeLeftText;

        public readonly ReactiveCommand UpdateTimer = new();
    }
}
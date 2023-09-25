using CodeBase.ECSCore;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CLevelTimeLeft : EntityComponent<CLevelTimeLeft>
    {
        [SerializeField] private TextMeshProUGUI _timeLeftText;

        public TextMeshProUGUI TimeLeftText => _timeLeftText;
    }
}
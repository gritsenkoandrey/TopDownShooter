using CodeBase.ECSCore;
using CodeBase.Utils;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CLevelTimeLeft : EntityComponent<CLevelTimeLeft>
    {
        [SerializeField] private TextMeshProUGUI _timeLeftText;

        public void SetTimeLeftText(int value) => _timeLeftText.text = FormatTime.SecondsToTime(value);
    }
}
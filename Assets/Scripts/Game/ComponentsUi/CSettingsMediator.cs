using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CSettingsMediator : EntityComponent<CSettingsMediator>
    {
        [SerializeField] private CToggle _vibrationToggle;

        public CToggle VibrationToggle => _vibrationToggle;
    }
}
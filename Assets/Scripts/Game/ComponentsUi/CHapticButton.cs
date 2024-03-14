using CodeBase.ECSCore;
using CodeBase.Infrastructure.Haptic.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CHapticButton : EntityComponent<CHapticButton>
    {
        [SerializeField] private Button _button;
        [SerializeField] private HapticType _hapticType;

        public Button Button => _button;
        public HapticType HapticType => _hapticType;
    }
}
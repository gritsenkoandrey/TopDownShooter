using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CRegenerationHealth : EntityComponent<CRegenerationHealth>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _text;

        public CanvasGroup CanvasGroup => _canvasGroup;
        public TextMeshProUGUI Text => _text;
        public bool IsActive { get; private set; }
        public ITarget Target { get; private set; }

        public void SetActive(bool isActive) => IsActive = isActive;
        public void SetTarget(ITarget target) => Target = target;
    }
}
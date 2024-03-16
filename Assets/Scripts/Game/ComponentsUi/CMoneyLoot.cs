using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using TMPro;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CMoneyLoot : EntityComponent<CMoneyLoot>
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private CanvasGroup _canvasGroup;

        public TextMeshProUGUI Text => _text;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public bool IsActive { get; private set; }
        public ITarget Target { get; private set; }

        public void SetTarget(ITarget target) => Target = target;
        public void SetActive(bool isActive) => IsActive = isActive;
    }
}
using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CUpgradeShop : EntityComponent<CUpgradeShop>
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private UpgradeButtonType[] _upgradeButtonType;

        public GameObject Root => _root;
        public CanvasGroup CanvasGroup => _canvasGroup;
        public UpgradeButtonType[] UpgradeButtonType => _upgradeButtonType;
    }
}
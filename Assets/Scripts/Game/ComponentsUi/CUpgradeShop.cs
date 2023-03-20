using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using UnityEngine;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CUpgradeShop : EntityComponent<CUpgradeShop>
    {
        [SerializeField] private Transform _root;
        [SerializeField] private UpgradeButtonType[] _upgradeButtonType;

        public Transform Root => _root;
        public UpgradeButtonType[] UpgradeButtonType => _upgradeButtonType;
        
        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
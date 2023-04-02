using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Game.ComponentsUi
{
    public sealed class CUpgradeShop : EntityComponent<CUpgradeShop>
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _show;
        [SerializeField] private GameObject _hide;
        [SerializeField] private UpgradeButtonType[] _upgradeButtonType;

        public Transform Root => _root;
        public Button Button => _button;
        public GameObject Show => _show;
        public GameObject Hide => _hide;
        public UpgradeButtonType[] UpgradeButtonType => _upgradeButtonType;
        public ReactiveProperty<bool> IsShowUpgradeShop { get; } = new(false);

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
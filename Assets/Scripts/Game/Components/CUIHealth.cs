using System.Collections.Generic;
using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CUIHealth : EntityComponent<CUIHealth>
    {
        [SerializeField] private Transform _root;
        [SerializeField] private GameObject _hp;

        public Transform Root => _root;
        public GameObject HP => _hp;

        public readonly Stack<GameObject> Healths = new();

        protected override void OnEntityCreate() { }
        protected override void OnEntityEnable() { }
        protected override void OnEntityDisable() { }
    }
}
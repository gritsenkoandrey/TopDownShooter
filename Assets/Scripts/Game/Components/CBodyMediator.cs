using System.Collections.Generic;
using CodeBase.ECSCore;
using CodeBase.Game.ComponentsUi;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CBodyMediator : EntityComponent<CBodyMediator>
    {
        [SerializeField] private SkinData[] _skins;

        private readonly List<SkinnedMeshRenderer> _skinnedMeshes = new();
        
        public SkinData[] Skins => _skins;
        public IReadOnlyList<SkinnedMeshRenderer> SkinnedMeshes => _skinnedMeshes;

        public void Add(SkinnedMeshRenderer skinnedMesh) => _skinnedMeshes.Add(skinnedMesh);
    }
}
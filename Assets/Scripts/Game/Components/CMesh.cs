using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CMesh : EntityComponent<CMesh>
    {
        [SerializeField] private Renderer _renderer;
        public Renderer Renderer => _renderer;
    }
}
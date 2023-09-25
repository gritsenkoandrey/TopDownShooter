using CodeBase.ECSCore;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CSelectMesh : EntityComponent<CSelectMesh>
    {
        [SerializeField] private GameObject[] _meshes;

        public GameObject[] Meshes => _meshes;
    }
}
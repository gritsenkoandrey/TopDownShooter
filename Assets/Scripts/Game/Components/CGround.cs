using CodeBase.ECSCore;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.Game.Components
{
    public sealed class CGround : EntityComponent<CGround>
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;

        public void BuildNavMesh() => _navMeshSurface.BuildNavMesh();
        public void UpdateNavMesh() => _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
    }
}
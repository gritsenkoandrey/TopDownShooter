using CodeBase.ECSCore;
using Unity.AI.Navigation;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CGround : EntityComponent<CGround>
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;
        public void BuildNavMesh() => _navMeshSurface.BuildNavMesh();
        public void UpdateNavMesh() => _navMeshSurface.UpdateNavMesh(_navMeshSurface.navMeshData);
    }
}
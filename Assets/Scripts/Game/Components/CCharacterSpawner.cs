using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CCharacterSpawner : EntityComponent<CCharacterSpawner>, IPosition
    {
        public Vector3 Position => transform.position;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}
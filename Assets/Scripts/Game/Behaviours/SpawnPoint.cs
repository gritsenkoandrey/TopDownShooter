using CodeBase.Game.Enums;
using CodeBase.Game.Interfaces;
using UnityEngine;

namespace CodeBase.Game.Behaviours
{
    public sealed class SpawnPoint : MonoBehaviour, ISpawnPoint
    {
        [SerializeField] private UnitType _unitType;
        [SerializeField] private ZombieType _zombieType;

        public UnitType UnitType => _unitType;
        public ZombieType ZombieType => _zombieType;
        public Vector3 Position => transform.position;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _unitType == UnitType.Character ? Color.green : Color.red;

            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}
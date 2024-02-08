using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Game.Weapon;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CUnitSpawner : EntityComponent<CUnitSpawner>
    {
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private UnitStats _unitStats;
        [SerializeField] private WeaponCharacteristic _weaponCharacteristic;
        
        public WeaponType WeaponType => _weaponType;
        public UnitStats UnitStats => _unitStats;
        public WeaponCharacteristic WeaponCharacteristic => _weaponCharacteristic;
        public Vector3 Position => transform.position;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}
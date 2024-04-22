using CodeBase.ECSCore;
using CodeBase.Game.Enums;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CTurretSpawner : EntityComponent<CTurretSpawner>
    {
        [SerializeField] private TurretType _turretType;
        [SerializeField] private TurretStats _turretStats;
        [SerializeField] private WeaponCharacteristic _weaponCharacteristic;
        
        public TurretType TurretType => _turretType;
        public TurretStats TurretStats => _turretStats;
        public WeaponCharacteristic WeaponCharacteristic => _weaponCharacteristic;
        public Vector3 Position => transform.position;
    }
}
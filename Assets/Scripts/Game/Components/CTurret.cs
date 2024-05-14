using CodeBase.ECSCore;
using CodeBase.Game.Interfaces;
using CodeBase.Infrastructure.StaticData.Data;
using UnityEngine;

namespace CodeBase.Game.Components
{
    public sealed class CTurret : EntityComponent<CTurret>, IEnemy
    {
        [SerializeField] private CRadar _radar;
        [SerializeField] private CStateMachine _stateMachine;
        [SerializeField] private CWeapon _weapon;
        [SerializeField] private CHealth _health;

        public CRadar Radar => _radar;
        public CStateMachine StateMachine => _stateMachine;
        public CWeapon Weapon => _weapon;
        public Transform Rotate => _weapon.transform;
        public CHealth Health => _health;
        public TurretStats Stats { get; set; }
        public Vector3 Position => transform.position;
        public float Height => Stats.Height;
        public float Scale => Stats.Scale;
        public int Loot => Stats.Money;
    }
}
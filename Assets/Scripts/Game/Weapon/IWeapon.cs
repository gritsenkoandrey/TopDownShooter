using System;
using CodeBase.Game.Interfaces;

namespace CodeBase.Game.Weapon
{
    public interface IWeapon : IDisposable
    {
        public void Attack(ITarget target = null);
        public bool CanAttack();
        public bool IsDetectThroughObstacle();
        public float AttackDistance();
        public float DetectionDistance();
    }
}
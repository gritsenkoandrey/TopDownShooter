using System;

namespace CodeBase.Game.Weapon
{
    public interface IWeapon : IDisposable
    {
        public void Attack();
        public bool CanAttack();
        public bool IsDetectThroughObstacle();
        public float AttackDistance();
    }
}
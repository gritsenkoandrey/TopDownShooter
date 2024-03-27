using System;

namespace CodeBase.Game.Interfaces
{
    public interface IWeapon : IDisposable
    {
        void Attack(ITarget target = null);
        bool CanAttack();
        bool IsDetectThroughObstacle();
        float AttackDistance();
        float DetectionDistance();
        float AimingSpeed();
    }
}
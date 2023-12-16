namespace CodeBase.Game.Weapon
{
    public interface IWeapon
    {
        public void Attack();
        public bool CanAttack();
        public bool IsDetectThroughObstacle();
        public float AttackDistance();
    }
}
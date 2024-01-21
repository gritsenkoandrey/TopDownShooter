using CodeBase.Game.Components;

namespace CodeBase.Game.Interfaces
{
    public interface ICharacter : IEntity, IHealth, IStateMachine
    {
        public CAnimator Animator { get; }
        public CWeaponMediator WeaponMediator { get; }
        public CMove Move { get; }
    }
}